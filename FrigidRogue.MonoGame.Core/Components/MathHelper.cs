using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigidRogue.MonoGame.Core.Components
{
    public static class MathHelper
    {
        /// <summary>
        /// Clamps the specified value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value which should be clamped.</param>
        /// <param name="min">The min limit.</param>
        /// <param name="max">The max limit.</param>
        /// <returns>
        /// <paramref name="value"/> clamped to the interval
        /// [<paramref name="min"/>, <paramref name="max"/>].
        /// </returns>
        /// <remarks>
        /// Values within the limits are not changed. Values exceeding the limits are cut off.
        /// </remarks>
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (min.CompareTo(max) > 0)
            {
                // min and max are swapped.
                var dummy = max;
                max = min;
                min = dummy;
            }

            if (value.CompareTo(min) < 0)
                value = min;
            else if (value.CompareTo(max) > 0)
                value = max;

            return value;
        }

        /// <overloads>
        /// <summary>
        /// Computes Sqrt(a*a + b*b) without underflow/overflow.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Computes Sqrt(a*a + b*b) without underflow/overflow (single-precision).
        /// </summary>
        /// <param name="cathetusA">Cathetus a.</param>
        /// <param name="cathetusB">Cathetus b.</param>
        /// <returns>The hypotenuse c, which is Sqrt(a*a + b*b).</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static float Hypotenuse(float cathetusA, float cathetusB)
        {
            float h = 0;
            if (Math.Abs(cathetusA) > Math.Abs(cathetusB))
            {
                h = cathetusB / cathetusA;
                h = (float)(Math.Abs(cathetusA) * Math.Sqrt(1 + h * h));
            }
            else if (cathetusB != 0)
            {
                h = cathetusA / cathetusB;
                h = (float)(Math.Abs(cathetusB) * Math.Sqrt(1 + h * h));
            }

            return h;
        }

        /// <summary>
        /// Computes Sqrt(a*a + b*b) without underflow/overflow (double-precision).
        /// </summary>
        /// <param name="cathetusA">Cathetus a.</param>
        /// <param name="cathetusB">Cathetus b.</param>
        /// <returns>The hypotenuse c, which is Sqrt(a*a + b*b).</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Hypotenuse(double cathetusA, double cathetusB)
        {
            double h = 0;
            if (Math.Abs(cathetusA) > Math.Abs(cathetusB))
            {
                h = cathetusB / cathetusA;
                h = Math.Abs(cathetusA) * Math.Sqrt(1 + h * h);
            }
            else if (cathetusB != 0)
            {
                h = cathetusA / cathetusB;
                h = Math.Abs(cathetusB) * Math.Sqrt(1 + h * h);
            }

            return h;
        }

        /// <summary>
        /// Swaps the content of two variables.
        /// </summary>
        /// <typeparam name="T">The type of the objects.</typeparam>
        /// <param name="obj1">First variable.</param>
        /// <param name="obj2">Second variable.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference")]
        public static void Swap<T>(ref T obj1, ref T obj2)
        {
            T temp = obj1;
            obj1 = obj2;
            obj2 = temp;
        }

        /// <overloads>
        /// <summary>
        /// Converts an angle value from degrees to radians.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Converts an angle value from degrees to radians (single-precision).
        /// </summary>
        /// <param name="degree">The angle in degrees.</param>
        /// <returns>The angle in radians.</returns>
        public static float ToRadians(float degree)
        {
            return degree * ConstantsF.Pi / 180;
        }

        /// <summary>
        /// Converts an angle value from degrees to radians (double-precision).
        /// </summary>
        /// <param name="degree">The angle in degrees.</param>
        /// <returns>The angle in radians.</returns>
        public static double ToRadians(double degree)
        {
            return degree * ConstantsD.Pi / 180;
        }

        /// <overloads>
        /// <summary>
        /// Converts an angle value from radians to degrees.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Converts an angle value from radians to degrees (single-precision).
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <returns>The angle in degrees.</returns>
        public static float ToDegrees(float radians)
        {
            return radians * 180 * ConstantsF.OneOverPi;
        }

        /// <summary>
        /// Converts an angle value from radians to degrees (double-precision).
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <returns>The angle in degrees.</returns>
        public static double ToDegrees(double radians)
        {
            return radians * 180 * ConstantsD.OneOverPi;
        }

        /// <summary>
        /// Returns the largest non-negative integer x such that 2<sup>x</sup> ≤ <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The largest non-negative integer x such that 2<sup>x</sup> ≤ <paramref name="value"/>.
        /// Exception: If <paramref name="value"/> is 0 then 0 is returned.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static uint Log2LessOrEqual(uint value)
        {
            // See Game Programming Gems 3, "Fast Base-2 Functions for Logarithms and Random Number Generation.

            uint testValue; // The value against which we test in the if condition.
            uint x;         // The value we are looking for.

            if (value >= 0x10000)
            {
                x = 16;
                testValue = 0x1000000;
            }
            else
            {
                x = 0;
                testValue = 0x100;
            }

            if (value >= testValue)
            {
                x += 8;
                testValue <<= 4;
            }
            else
            {
                testValue >>= 4;
            }

            if (value >= testValue)
            {
                x += 4;
                testValue <<= 2;
            }
            else
            {
                testValue >>= 2;
            }

            if (value >= testValue)
            {
                x += 2;
                testValue <<= 1;
            }
            else
            {
                testValue >>= 1;
            }

            if (value >= testValue)
            {
                x += 1;
            }

            return x;
        }

        /// <summary>
        /// Returns the smallest non-negative integer x such that 2<sup>x</sup> ≥ <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The smallest non-negative integer x such that 2<sup>x</sup> ≥ <paramref name="value"/>.
        /// Exception: If <paramref name="value"/> is 0, 0 is returned.
        /// </returns>
        public static uint Log2GreaterOrEqual(uint value)
        {
            // See Game Programming Gems 3, "Fast Base-2 Functions for Logarithms and Random Number Generation.
            if (value > 0x80000000)
                return 32;

            uint testValue; // The value against which we test in the if condition.
            uint x;         // The value we are looking for.

            if (value > 0x8000)
            {
                x = 16;
                testValue = 0x800000;
            }
            else
            {
                x = 0;
                testValue = 0x80;
            }

            if (value > testValue)
            {
                x += 8;
                testValue <<= 4;
            }
            else
            {
                testValue >>= 4;
            }

            if (value > testValue)
            {
                x += 4;
                testValue <<= 2;
            }
            else
            {
                testValue >>= 2;
            }

            if (value > testValue)
            {
                x += 2;
                testValue <<= 1;
            }
            else
            {
                testValue >>= 1;
            }

            if (value > testValue)
            {
                x += 1;
            }

            return x;
        }

        /// <summary>
        /// Creates the smallest bitmask that is greater than or equal to the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A bitmask where the left bits are 0 and the right bits are 1. The value of the bitmask
        /// is ≥ <paramref name="value"/>.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This result can also be interpreted as finding the smallest x such that 2<sup>x</sup> &gt; 
        /// <paramref name="value"/> and returning 2<sup>x</sup> - 1.
        /// </para>
        /// <para>
        /// Another useful application: Bitmask(x) + 1 returns the next power of 2 that is greater than 
        /// x.
        /// </para>
        /// </remarks>
        public static uint Bitmask(uint value)
        {
            // Example:                 value = 10000000 00000000 00000000 00000000
            value |= (value >> 1);   // value = 11000000 00000000 00000000 00000000
            value |= (value >> 2);   // value = 11110000 00000000 00000000 00000000
            value |= (value >> 4);   // value = 11111111 00000000 00000000 00000000
            value |= (value >> 8);   // value = 11111111 11111111 00000000 00000000
            value |= (value >> 16);  // value = 11111111 11111111 11111111 11111111
            return value;
        }

        /// <summary>
        /// Determines whether the specified value is a power of two.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is a power of two; otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "value-1")]
        public static bool IsPowerOf2(int value)
        {
            // See http://stackoverflow.com/questions/600293/how-to-check-if-a-number-is-a-power-of-2
            return (value != 0) && (value & (value - 1)) == 0;
        }

        /// <summary>
        /// Returns the smallest power of two that is greater than the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The smallest power of two (2<sup>x</sup>) that is greater than <paramref name="value"/>.
        /// </returns>
        /// <remarks>
        /// For example, <c>NextPowerOf2(7)</c> is <c>8</c> and <c>NextPowerOf2(8)</c> is <c>16</c>.
        /// </remarks>
        public static uint NextPowerOf2(uint value)
        {
            return Bitmask(value) + 1;
        }

        /// <overloads>
        /// <summary>
        /// Computes the Gaussian function y = k * e^( -(x-μ)<sup>2</sup>/(2σ<sup>2</sup>).
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Computes the Gaussian function y = k * e^( -(x-μ)<sup>2</sup>/(2σ<sup>2</sup>) 
        /// (single precision).
        /// </summary>
        /// <param name="x">The argument x.</param>
        /// <param name="coefficient">The coefficient k.</param>
        /// <param name="expectedValue">The expected value μ.</param>
        /// <param name="standardDeviation">The standard deviation σ.</param>
        /// <returns>The height of the Gaussian bell curve at x.</returns>
        /// <remarks>
        /// This method computes the Gaussian bell curve.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static float Gaussian(float x, float coefficient, float expectedValue, float standardDeviation)
        {
            float xMinusExpected = x - expectedValue;
            return coefficient * (float)Math.Exp(-xMinusExpected * xMinusExpected
                                                 / (2 * standardDeviation * standardDeviation));
        }

        /// <summary>
        /// <summary>
        /// Computes the Gaussian function y = k * e^( -(x-μ)<sup>2</sup>/(2σ<sup>2</sup>) 
        /// (double-precision).
        /// </summary>
        /// </summary>
        /// <param name="x">The argument x.</param>
        /// <param name="coefficient">The coefficient k.</param>
        /// <param name="expectedValue">The expected value μ.</param>
        /// <param name="standardDeviation">The standard deviation σ.</param>
        /// <returns>The height of the Gaussian bell curve at x.</returns>
        /// <remarks>
        /// This method computes the Gaussian bell curve.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Gaussian(double x, double coefficient, double expectedValue, double standardDeviation)
        {
            double xMinusExpected = x - expectedValue;
            return coefficient * Math.Exp(-xMinusExpected * xMinusExpected
                                          / (2 * standardDeviation * standardDeviation));
        }

        /// <summary>
        /// Computes the binomial coefficient of (n, k), also read as "n choose k".
        /// </summary>
        /// <param name="n">n, must be a value equal to or greater than 0.</param>
        /// <param name="k">k, a value in the range [0, <paramref name="n"/>].</param>
        /// <returns>
        /// The binomial coefficient.
        /// </returns>
        /// <remarks>
        /// This method returns a binomial coefficient. The result is the k'th element in the n'th row
        /// of Pascal's triangle (using zero-based indices for k and n). This method returns 0 for
        /// negative <paramref name="n"/>.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static long BinomialCoefficient(int n, int k)
        {
            // See http://blog.plover.com/math/choose.html.

            if (k < 0 || k > n)
                return 0;

            long r = 1;
            long d;
            for (d = 1; d <= k; d++)
            {
                r *= n--;
                r /= d;
            }
            return r;
        }

        /// <overloads>
        /// <summary>
        /// Calculates the fractional part of a specified floating-point number.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Calculates the fractional part of a specified single-precision floating-point number.
        /// </summary>
        /// <param name="f">The number.</param>
        /// <returns>The fractional part of <paramref name="f"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static float Frac(float f)
        {
            return f - (float)Math.Floor(f);
        }

        /// <summary>
        /// Calculates the fractional part of a specified double-precision floating-point number.
        /// </summary>
        /// <param name="d">The number.</param>
        /// <returns>The fractional part of <paramref name="d"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Frac(double d)
        {
            return d - Math.Floor(d);
        }
    }
}
