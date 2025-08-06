using System.ComponentModel.Design;

using Castle.Core;

using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Services;

using GoRogue.Random;

using ShaiRandom.Generators;

namespace FrigidRogue.MonoGame.Core.Components
{
    public class ContextualEnhancedRandom : IContextualEnhancedRandom
    {
        public static IContextualEnhancedRandom FromGlobalRandom => GlobalRandom.DefaultRNG as ContextualEnhancedRandom
            ?? throw new InvalidOperationException("GlobalRandom.DefaultRNG is not a ContextualEnhancedRandom instance.");

        private MizuchiRandom _mizuchiRandom;

        public int StateCount => _mizuchiRandom.StateCount;
        public bool SupportsReadAccess => _mizuchiRandom.SupportsReadAccess;
        public bool SupportsWriteAccess => _mizuchiRandom.SupportsWriteAccess;
        public bool SupportsSkip => _mizuchiRandom.SupportsSkip;
        public bool SupportsLeap => _mizuchiRandom.SupportsLeap;
        public bool SupportsPrevious => _mizuchiRandom.SupportsPrevious;
        public string DefaultTag => _mizuchiRandom.DefaultTag;

        public ContextualEnhancedRandom()
        {
        }

        public void Reset(ulong seed)
        {
            _mizuchiRandom = new MizuchiRandom(seed);
        }

        public MizuchiRandom GetSaveState()
        {
            return new MizuchiRandom(_mizuchiRandom.StateA, _mizuchiRandom.StateB);
        }

        public void SetLoadState(MizuchiRandom loadState)
        {
            _mizuchiRandom = new MizuchiRandom(loadState.StateA, loadState.StateB);
        }

        public int NextInt()
            => _mizuchiRandom.NextInt();

        public int NextInt(int maxValue)
            => _mizuchiRandom.NextInt(maxValue);

        public int NextInt(int minValue, int maxValue)
            => _mizuchiRandom.NextInt(minValue, maxValue);

        public double NextDouble()
            => _mizuchiRandom.NextDouble();

        public float NextFloat()
            => _mizuchiRandom.NextFloat();

        public bool NextBool()
            => _mizuchiRandom.NextBool();

        public bool NextBool(float probability)
            => _mizuchiRandom.NextBool(probability);

        public void Seed(ulong seed)
            => _mizuchiRandom.Seed(seed);

        public IEnhancedRandom Copy()
            => _mizuchiRandom.Copy();

        public string StringSerialize()
            => _mizuchiRandom.StringSerialize();

        public IEnhancedRandom StringDeserialize(ReadOnlySpan<char> data)
            => _mizuchiRandom.StringDeserialize(data);

        public ulong SelectState(int selection)
            => _mizuchiRandom.SelectState(selection);

        public void SetSelectedState(int selection, ulong value)
            => _mizuchiRandom.SetSelectedState(selection, value);

        public ulong NextULong()
            => _mizuchiRandom.NextULong();

        public long NextLong()
            => _mizuchiRandom.NextLong();

        public ulong NextULong(ulong bound)
            => _mizuchiRandom.NextULong(bound);

        public long NextLong(long outerBound)
            => _mizuchiRandom.NextLong(outerBound);

        public ulong NextULong(ulong inner, ulong outer)
            => _mizuchiRandom.NextULong(inner, outer);

        public long NextLong(long inner, long outer)
            => _mizuchiRandom.NextLong(inner, outer);

        public uint NextBits(int bits)
            => _mizuchiRandom.NextBits(bits);

        public void NextBytes(Span<byte> bytes)
            => _mizuchiRandom.NextBytes(bytes);

        public uint NextUInt()
            => _mizuchiRandom.NextUInt();

        public uint NextUInt(uint bound)
            => _mizuchiRandom.NextUInt(bound);

        public uint NextUInt(uint innerBound, uint outerBound)
            => _mizuchiRandom.NextUInt(innerBound, outerBound);

        public float NextFloat(float outerBound)
            => _mizuchiRandom.NextFloat(outerBound);

        public float NextFloat(float innerBound, float outerBound)
            => _mizuchiRandom.NextFloat(innerBound, outerBound);

        public double NextDouble(double outerBound)
            => _mizuchiRandom.NextDouble(outerBound);

        public double NextDouble(double innerBound, double outerBound)
            => _mizuchiRandom.NextDouble(innerBound, outerBound);

        public float NextSparseFloat()
            => _mizuchiRandom.NextSparseFloat();

        public float NextSparseFloat(float outerBound)
            => _mizuchiRandom.NextSparseFloat(outerBound);

        public float NextSparseFloat(float innerBound, float outerBound)
            => _mizuchiRandom.NextSparseFloat(innerBound, outerBound);

        public double NextSparseDouble()
            => _mizuchiRandom.NextSparseDouble();

        public double NextSparseDouble(double outerBound)
            => _mizuchiRandom.NextSparseDouble(outerBound);

        public double NextSparseDouble(double innerBound, double outerBound)
            => _mizuchiRandom.NextSparseDouble(innerBound, outerBound);

        public decimal NextDecimal()
            => _mizuchiRandom.NextDecimal();

        public decimal NextDecimal(decimal outerBound)
            => _mizuchiRandom.NextDecimal(outerBound);

        public decimal NextDecimal(decimal innerBound, decimal outerBound)
            => _mizuchiRandom.NextDecimal(innerBound, outerBound);

        public double NextInclusiveDouble()
            => _mizuchiRandom.NextInclusiveDouble();

        public double NextInclusiveDouble(double outerBound)
            => _mizuchiRandom.NextInclusiveDouble(outerBound);

        public double NextInclusiveDouble(double innerBound, double outerBound)
            => _mizuchiRandom.NextInclusiveDouble(innerBound, outerBound);

        public float NextInclusiveFloat()
            => _mizuchiRandom.NextInclusiveFloat();

        public float NextInclusiveFloat(float outerBound)
            => _mizuchiRandom.NextInclusiveFloat(outerBound);

        public float NextInclusiveFloat(float innerBound, float outerBound)
            => _mizuchiRandom.NextInclusiveFloat(innerBound, outerBound);

        public decimal NextInclusiveDecimal()
            => _mizuchiRandom.NextInclusiveDecimal();

        public decimal NextInclusiveDecimal(decimal outerBound)
            => _mizuchiRandom.NextInclusiveDecimal(outerBound);

        public decimal NextInclusiveDecimal(decimal innerBound, decimal outerBound)
            => _mizuchiRandom.NextInclusiveDecimal(innerBound, outerBound);
        public double NextExclusiveDouble()
            => _mizuchiRandom.NextExclusiveDouble();

        public double NextExclusiveDouble(double outerBound)
            => _mizuchiRandom.NextExclusiveDouble(outerBound);

        public double NextExclusiveDouble(double innerBound, double outerBound)
            => _mizuchiRandom.NextExclusiveDouble(innerBound, outerBound);

        public float NextExclusiveFloat()
            => _mizuchiRandom.NextExclusiveFloat();

        public float NextExclusiveFloat(float outerBound)
            => _mizuchiRandom.NextExclusiveFloat(outerBound);

        public float NextExclusiveFloat(float innerBound, float outerBound)
            => _mizuchiRandom.NextExclusiveFloat(innerBound, outerBound);

        public decimal NextExclusiveDecimal()
            => _mizuchiRandom.NextExclusiveDecimal();

        public decimal NextExclusiveDecimal(decimal outerBound)
            => _mizuchiRandom.NextExclusiveDecimal(outerBound);

        public decimal NextExclusiveDecimal(decimal innerBound, decimal outerBound)
            => _mizuchiRandom.NextExclusiveDecimal(innerBound, outerBound);

        public ulong Skip(ulong distance)
            => _mizuchiRandom.Skip(distance);

        public ulong PreviousULong()
            => _mizuchiRandom.PreviousULong();

        public ulong Leap()
            => _mizuchiRandom.Leap();

        public virtual bool NextBool(string context)
        {
            return NextBool();
        }

        public virtual int NextInt(int upper, string context)
        {
            return NextInt(upper);
        }

        public virtual int NextInt(int lower, int upper, string context)
        {
            return NextInt(lower, upper);
        }
    }
}