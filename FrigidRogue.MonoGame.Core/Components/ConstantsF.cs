using System.Diagnostics.CodeAnalysis;

namespace FrigidRogue.MonoGame.Core.Components
{
    /// <summary>
    /// Provides useful constants (single-precision).
    /// </summary>
    public static class ConstantsF
    {
        /// <summary>Represents the mathematical constant e.</summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public const float E = 2.718281828459045235360287471352f;

        /// <summary>Represents the logarithm base 10 of e.</summary>
        public const float Log10OfE = 0.4342944819032518276511289189165f;

        /// <summary>Represents the logarithm base 2 of e.</summary>
        public const float Log2OfE = 1.4426950408889634073599246810019f;

        /// <summary>Represents the natural logarithm of 2.</summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public const float Ln2 = 0.69314718055994530941723212145818f;

        /// <summary>Represents the natural logarithm of 10.</summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public const float Ln10 = 2.3025850929940456840179914546844f;

        /// <summary>Represents one divided by the mathematical constant π.</summary>
        public const float OneOverPi = 0.31830988618379067153776752674503f;

        /// <summary>Represents the mathematical constant π.</summary>
        public const float Pi = 3.1415926535897932384626433832795f;

        /// <summary>Represents the mathematical constant π divided by two.</summary>
        public const float PiOver2 = 1.5707963267948966192313216916398f;

        /// <summary>Represents the mathematical constant π divided by four.</summary>
        public const float PiOver4 = 0.78539816339744830961566084581988f;

        /// <summary>Represents the mathematical constant π times two.</summary>
        public const float TwoPi = 6.283185307179586476925286766559f;
    }
}

