using System;
using System.Reflection;

using Newtonsoft.Json;

using Troschuetz.Random.Generators;

namespace FrigidRogue.MonoGame.Core.Components
{
    public class XorShift128GeneratorJsonConverter : JsonConverter<XorShift128Generator>
    {
        public override void WriteJson(JsonWriter writer, XorShift128Generator value, JsonSerializer serializer)
        {
            var randomNumberSaveData = GetRandomNumberSaveData(value);

            serializer.Serialize(writer, randomNumberSaveData);
        }

        public override XorShift128Generator ReadJson(
            JsonReader reader,
            Type objectType,
            XorShift128Generator existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        )
        {
            var randomNumberSaveData = serializer.Deserialize<RandomNumberSaveData>(reader);

            var xorShift128Generator = hasExistingValue ? existingValue : new XorShift128Generator();

            return LoadRandomNumberSaveData(xorShift128Generator, randomNumberSaveData);
        }

        private RandomNumberSaveData GetRandomNumberSaveData(XorShift128Generator xorShift128Generator)
        {
            var xorShift128GeneratorType = typeof(XorShift128Generator);
            var abstractGeneratorType = typeof(AbstractGenerator);

            var bytesAvailableFieldBool = xorShift128GeneratorType.GetField("_bytesAvailable", BindingFlags.NonPublic | BindingFlags.Instance);
            var xFieldULong = xorShift128GeneratorType.GetField("_x", BindingFlags.NonPublic | BindingFlags.Instance);
            var yFieldULong = xorShift128GeneratorType.GetField("_y", BindingFlags.NonPublic | BindingFlags.Instance);
            var bitBufferFieldUInt = abstractGeneratorType.GetField("_bitBuffer", BindingFlags.NonPublic | BindingFlags.Instance);
            var bitCountFieldInt = abstractGeneratorType.GetField("_bitCount", BindingFlags.NonPublic | BindingFlags.Instance);

            var randomNumberSaveData = new RandomNumberSaveData
            {
                BytesAvailable = (bool)bytesAvailableFieldBool.GetValue(xorShift128Generator),
                X = (ulong)xFieldULong.GetValue(xorShift128Generator),
                Y = (ulong)yFieldULong.GetValue(xorShift128Generator),
                BitBuffer = (uint)bitBufferFieldUInt.GetValue(xorShift128Generator),
                BitCount = (int)bitCountFieldInt.GetValue(xorShift128Generator),
                Seed = xorShift128Generator.Seed
            };

            return randomNumberSaveData;
        }

        private XorShift128Generator LoadRandomNumberSaveData(XorShift128Generator existingValue, RandomNumberSaveData randomNumberSaveData)
        {
            existingValue.Seed = randomNumberSaveData.Seed;

            var xorShift128GeneratorType = typeof(XorShift128Generator);
            var abstractGeneratorType = typeof(AbstractGenerator);

            var bytesAvailableFieldBool = xorShift128GeneratorType.GetField("_bytesAvailable", BindingFlags.NonPublic | BindingFlags.Instance);
            var xFieldULong = xorShift128GeneratorType.GetField("_x", BindingFlags.NonPublic | BindingFlags.Instance);
            var yFieldULong = xorShift128GeneratorType.GetField("_y", BindingFlags.NonPublic | BindingFlags.Instance);
            var bitBufferFieldUInt = abstractGeneratorType.GetField("_bitBuffer", BindingFlags.NonPublic | BindingFlags.Instance);
            var bitCountFieldInt = abstractGeneratorType.GetField("_bitCount", BindingFlags.NonPublic | BindingFlags.Instance);

            bytesAvailableFieldBool.SetValue(existingValue, randomNumberSaveData.BytesAvailable);
            xFieldULong.SetValue(existingValue, randomNumberSaveData.X);
            yFieldULong.SetValue(existingValue, randomNumberSaveData.Y);
            bitBufferFieldUInt.SetValue(existingValue, randomNumberSaveData.BitBuffer);
            bitCountFieldInt.SetValue(existingValue, randomNumberSaveData.BitCount);

            return existingValue;
        }
    }
}