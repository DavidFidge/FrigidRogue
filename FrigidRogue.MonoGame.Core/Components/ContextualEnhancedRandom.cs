using Castle.Core;

using FrigidRogue.MonoGame.Core.Interfaces.Components;

using GoRogue.Random;

using ShaiRandom.Generators;

namespace FrigidRogue.MonoGame.Core.Components
{
    public class ContextualEnhancedRandom : IContextualEnhancedRandom
    {
        public static IContextualEnhancedRandom FromGlobalRandom => GlobalRandom.DefaultRNG as ContextualEnhancedRandom
            ?? throw new InvalidOperationException("GlobalRandom.DefaultRNG is not a ContextualEnhancedRandom instance.");

        private IEnhancedRandom _enhancedRandom;

        public int StateCount => _enhancedRandom.StateCount;
        public bool SupportsReadAccess => _enhancedRandom.SupportsReadAccess;
        public bool SupportsWriteAccess => _enhancedRandom.SupportsWriteAccess;
        public bool SupportsSkip => _enhancedRandom.SupportsSkip;
        public bool SupportsLeap => _enhancedRandom.SupportsLeap;
        public bool SupportsPrevious => _enhancedRandom.SupportsPrevious;
        public string DefaultTag => _enhancedRandom.DefaultTag;

        public IEnhancedRandom EnhancedRandom { get; set; }

        public ContextualEnhancedRandom(IEnhancedRandom enhancedRandom)
        {
            _enhancedRandom = enhancedRandom;
        }

        public int NextInt()
            => _enhancedRandom.NextInt();

        public int NextInt(int maxValue)
            => _enhancedRandom.NextInt(maxValue);

        public int NextInt(int minValue, int maxValue)
            => _enhancedRandom.NextInt(minValue, maxValue);

        public double NextDouble()
            => _enhancedRandom.NextDouble();

        public float NextFloat()
            => _enhancedRandom.NextFloat();

        public bool NextBool()
            => _enhancedRandom.NextBool();

        public bool NextBool(float probability)
            => _enhancedRandom.NextBool(probability);

        public void Seed(ulong seed)
            => _enhancedRandom.Seed(seed);

        public IEnhancedRandom Copy()
            => _enhancedRandom.Copy();

        public string StringSerialize()
            => _enhancedRandom.StringSerialize();

        public IEnhancedRandom StringDeserialize(ReadOnlySpan<char> data)
            => _enhancedRandom.StringDeserialize(data);

        public ulong SelectState(int selection)
            => _enhancedRandom.SelectState(selection);

        public void SetSelectedState(int selection, ulong value)
            => _enhancedRandom.SetSelectedState(selection, value);

        public ulong NextULong()
            => _enhancedRandom.NextULong();

        public long NextLong()
            => _enhancedRandom.NextLong();

        public ulong NextULong(ulong bound)
            => _enhancedRandom.NextULong(bound);

        public long NextLong(long outerBound)
            => _enhancedRandom.NextLong(outerBound);

        public ulong NextULong(ulong inner, ulong outer)
            => _enhancedRandom.NextULong(inner, outer);

        public long NextLong(long inner, long outer)
            => _enhancedRandom.NextLong(inner, outer);

        public uint NextBits(int bits)
            => _enhancedRandom.NextBits(bits);

        public void NextBytes(Span<byte> bytes)
            => _enhancedRandom.NextBytes(bytes);

        public uint NextUInt()
            => _enhancedRandom.NextUInt();

        public uint NextUInt(uint bound)
            => _enhancedRandom.NextUInt(bound);

        public uint NextUInt(uint innerBound, uint outerBound)
            => _enhancedRandom.NextUInt(innerBound, outerBound);

        public float NextFloat(float outerBound)
            => _enhancedRandom.NextFloat(outerBound);

        public float NextFloat(float innerBound, float outerBound)
            => _enhancedRandom.NextFloat(innerBound, outerBound);

        public double NextDouble(double outerBound)
            => _enhancedRandom.NextDouble(outerBound);

        public double NextDouble(double innerBound, double outerBound)
            => _enhancedRandom.NextDouble(innerBound, outerBound);

        public float NextSparseFloat()
            => _enhancedRandom.NextSparseFloat();

        public float NextSparseFloat(float outerBound)
            => _enhancedRandom.NextSparseFloat(outerBound);

        public float NextSparseFloat(float innerBound, float outerBound)
            => _enhancedRandom.NextSparseFloat(innerBound, outerBound);

        public double NextSparseDouble()
            => _enhancedRandom.NextSparseDouble();

        public double NextSparseDouble(double outerBound)
            => _enhancedRandom.NextSparseDouble(outerBound);

        public double NextSparseDouble(double innerBound, double outerBound)
            => _enhancedRandom.NextSparseDouble(innerBound, outerBound);

        public decimal NextDecimal()
            => _enhancedRandom.NextDecimal();

        public decimal NextDecimal(decimal outerBound)
            => _enhancedRandom.NextDecimal(outerBound);

        public decimal NextDecimal(decimal innerBound, decimal outerBound)
            => _enhancedRandom.NextDecimal(innerBound, outerBound);

        public double NextInclusiveDouble()
            => _enhancedRandom.NextInclusiveDouble();

        public double NextInclusiveDouble(double outerBound)
            => _enhancedRandom.NextInclusiveDouble(outerBound);

        public double NextInclusiveDouble(double innerBound, double outerBound)
            => _enhancedRandom.NextInclusiveDouble(innerBound, outerBound);

        public float NextInclusiveFloat()
            => _enhancedRandom.NextInclusiveFloat();

        public float NextInclusiveFloat(float outerBound)
            => _enhancedRandom.NextInclusiveFloat(outerBound);

        public float NextInclusiveFloat(float innerBound, float outerBound)
            => _enhancedRandom.NextInclusiveFloat(innerBound, outerBound);

        public decimal NextInclusiveDecimal()
            => _enhancedRandom.NextInclusiveDecimal();

        public decimal NextInclusiveDecimal(decimal outerBound)
            => _enhancedRandom.NextInclusiveDecimal(outerBound);

        public decimal NextInclusiveDecimal(decimal innerBound, decimal outerBound)
            => _enhancedRandom.NextInclusiveDecimal(innerBound, outerBound);
        public double NextExclusiveDouble()
            => _enhancedRandom.NextExclusiveDouble();

        public double NextExclusiveDouble(double outerBound)
            => _enhancedRandom.NextExclusiveDouble(outerBound);

        public double NextExclusiveDouble(double innerBound, double outerBound)
            => _enhancedRandom.NextExclusiveDouble(innerBound, outerBound);

        public float NextExclusiveFloat()
            => _enhancedRandom.NextExclusiveFloat();

        public float NextExclusiveFloat(float outerBound)
            => _enhancedRandom.NextExclusiveFloat(outerBound);

        public float NextExclusiveFloat(float innerBound, float outerBound)
            => _enhancedRandom.NextExclusiveFloat(innerBound, outerBound);

        public decimal NextExclusiveDecimal()
            => _enhancedRandom.NextExclusiveDecimal();

        public decimal NextExclusiveDecimal(decimal outerBound)
            => _enhancedRandom.NextExclusiveDecimal(outerBound);

        public decimal NextExclusiveDecimal(decimal innerBound, decimal outerBound)
            => _enhancedRandom.NextExclusiveDecimal(innerBound, outerBound);

        public ulong Skip(ulong distance)
            => _enhancedRandom.Skip(distance);

        public ulong PreviousULong()
            => _enhancedRandom.PreviousULong();

        public ulong Leap()
            => _enhancedRandom.Leap();

        public virtual bool NextBool(string context)
        {
            return NextBool();
        }
    }
}