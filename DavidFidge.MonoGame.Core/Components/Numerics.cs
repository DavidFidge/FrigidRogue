using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace DavidFidge.MonoGame.Core.Components
{
    public static class Numeric
    {
        // Notes:
        // - Numeric.AreEqual() methods can be optimized, which might be necessary in extreme
        //   cases (e.g. raytracing). See Dawson's and Lomont's compare methods mentioned in:
        //   Floating-Point Tricks. Improving Performance with IEEE Floating Point, Game Programming Gems 2, pp. 167

        /*
        // ----- Constants from float.h (for reference)
        private const int DBL_DIG = 15;                             // # of decimal digits of precision
        private const double DBL_EPSILON = 2.2204460492503131e-016; // smallest such that 1.0+DBL_EPSILON != 1.0
        private const int DBL_MANT_DIG = 53;                        // # of bits in mantissa
        private const double DBL_MAX = 1.7976931348623158e+308;     // max value
        private const int DBL_MAX_10_EXP = 308;                     // max decimal exponent
        private const int DBL_MAX_EXP = 1024;                       // max binary exponent
        private const double DBL_MIN = 2.2250738585072014e-308;     // min positive value
        private const int DBL_MIN_10_EXP = -307;                    // min decimal exponent
        private const int DBL_MIN_EXP = -1021;                      // min binary exponent
        private const int _DBL_RADIX = 2;                           // exponent radix

        private const int FLT_DIG = 6;                              // # of decimal digits of precision
        private const float FLT_EPSILON = 1.192092896e-07F;         // smallest such that 1.0+FLT_EPSILON != 1.0
        private const int FLT_MANT_DIG = 24;                        // # of bits in mantissa
        private const float FLT_MAX = 3.402823466e+38F;             // max value
        private const int FLT_MAX_10_EXP = 38;                      // max decimal exponent
        private const int FLT_MAX_EXP = 128;                        // max binary exponent
        private const float FLT_MIN = 1.175494351e-38F;             // min positive value
        private const int FLT_MIN_10_EXP = -37;                     // min decimal exponent
        private const int FLT_MIN_EXP = -125;                       // min binary exponent
        private const int FLT_RADIX = 2;                            // exponent radix
        */

        // Epsilon values from other projects: EpsilonF = 1e-7; EpsilonD = 9e-16;
        // According to our unit tests the double epsilon is to small. 
        // Following epsilon values were appropriate for typical game applications and 3D simulations.
        private static float _epsilonF = 1e-5f;
        private static float _epsilonFSquared = 1e-5f * 1e-5f;
        private static double _epsilonD = 1e-12;
        private static double _epsilonDSquared = 1e-12 * 1e-12;


        /// <summary>
        /// Gets or sets the tolerance value used for comparison of <see langword="float"/> values.
        /// </summary>
        /// <value>The epsilon for single-precision floating-point.</value>
        /// <remarks>
        /// This value can be changed to set a new value for all subsequent comparisons.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is negative or 0.
        /// </exception>
        public static float EpsilonF
        {
            get { return _epsilonF; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("value", "The tolerance value must be greater than 0.");

                _epsilonF = value;
                _epsilonFSquared = value * value;
            }
        }

        /// <summary>
        /// Gets the squared tolerance value used for comparison of <see langword="float"/> values.
        /// (<see cref="EpsilonF"/> * <see cref="EpsilonF"/>).
        /// </summary>
        /// <value>The squared epsilon for single-precision floating-point.</value>
        public static float EpsilonFSquared
        {
            get { return _epsilonFSquared; }
        }

        /// <summary>
        /// Gets or sets the tolerance value used for comparison of <see langword="double"/> values.
        /// </summary>
        /// <value>The epsilon for double-precision floating-point.</value>
        /// <remarks>
        /// This value can be changed to set a new value for all subsequent comparisons.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is negative or 0.
        /// </exception>
        public static double EpsilonD
        {
            get { return _epsilonD; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("value", "The epsilon tolerance value must be greater than 0.");

                _epsilonD = value;
                _epsilonDSquared = value * value;
            }
        }

        /// <summary>
        /// Gets the squared tolerance value used for comparison of <see langword="double"/> values.
        /// (<see cref="EpsilonD"/> * <see cref="EpsilonD"/>).
        /// </summary>
        /// <value>The squared epsilon for double-precision floating-point.</value>
        public static double EpsilonDSquared
        {
            get { return _epsilonDSquared; }
        }

        /// <overloads>
        /// <summary>
        /// Determines whether two values are equal (regarding a given tolerance).
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Determines whether two values are equal (regarding the tolerance <see cref="EpsilonF"/>).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>
        /// <see langword="true"/> if the specified values are equal (within the tolerance); otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// <strong>Important:</strong> When at least one of the parameters is a 
        /// <see cref="Single.NaN"/> the result is undefined. Such cases should be handled explicitly
        /// by the calling application.
        /// </remarks>
        public static bool AreEqual(float value1, float value2)
        {
            // Infinity values have to be handled carefully because the check with the epsilon tolerance
            // does not work there. Check for equality in case they are infinite:
            if (value1 == value2)
                return true;

            // Scale epsilon proportional the given values.
            float epsilon = _epsilonF * (Math.Abs(value1) + Math.Abs(value2) + 1.0f);
            float delta = value1 - value2;
            return (-epsilon < delta) && (delta < epsilon);

            // We could also use ... Abs(v1 - v2) <= _epsilonF * Max(Abs(v1), Abs(v2), 1)
        }

        /// <summary>
        /// Determines whether two values are equal (regarding the tolerance <see cref="EpsilonD"/>).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>
        /// <see langword="true"/> if the specified values are equal (within the tolerance); otherwise,
        /// <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// <strong>Important:</strong> When at least one of the parameters is a 
        /// <see cref="Double.NaN"/> the result is undefined. Such cases should be handled explicitly by
        /// the calling application.
        /// </remarks>
        public static bool AreEqual(double value1, double value2)
        {
            // Infinity values have to be handled carefully because the check with the epsilon tolerance
            // does not work there. Check for equality in case they are infinite:
            if (value1 == value2)
                return true;

            // Scale epsilon proportional the given values.
            double epsilon = _epsilonD * (Math.Abs(value1) + Math.Abs(value2) + 1.0);
            double delta = value1 - value2;
            return (-epsilon < delta) && (delta < epsilon);

            // We could also use ... Abs(v1 - v2) <= _epsilonF * Max(Abs(v1), Abs(v2), 1)
        }

        /// <summary>
        /// Determines whether two values are equal (regarding a specific tolerance).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>
        /// <see langword="true"/> if the specified values are equal (within the tolerance); otherwise,
        /// <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// <strong>Important:</strong> When at least one of the parameters is a 
        /// <see cref="Single.NaN"/> the result is undefined. Such cases should be handled explicitly by
        /// the calling application.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="epsilon"/> is negative or 0.
        /// </exception>
        public static bool AreEqual(float value1, float value2, float epsilon)
        {
            if (epsilon <= 0.0f)
                throw new ArgumentOutOfRangeException("epsilon", "Epsilon value must be greater than 0.");

            // Infinity values have to be handled carefully because the check with the epsilon tolerance
            // does not work there. Check for equality in case they are infinite:
            if (value1 == value2)
                return true;

            float delta = value1 - value2;
            return (-epsilon < delta) && (delta < epsilon);
        }

        /// <summary>
        /// Determines whether two values are equal (regarding a specific tolerance).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>
        /// <see langword="true"/> if the specified values are equal (within the tolerance); otherwise,
        /// <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// <strong>Important:</strong> When at least one of the parameters is a 
        /// <see cref="Double.NaN"/> the result is undefined. Such cases should be handled explicitly by
        /// the calling application.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="epsilon"/> is negative or 0.
        /// </exception>
        public static bool AreEqual(double value1, double value2, double epsilon)
        {
            if (epsilon <= 0.0)
                throw new ArgumentOutOfRangeException("epsilon", "Epsilon value must be greater than 0.");

            // Infinity values have to be handled carefully because the check with the epsilon tolerance
            // does not work there. Check for equality in case they are infinite:
            if (value1 == value2)
                return true;

            double delta = value1 - value2;
            return (-epsilon < delta) && (delta < epsilon);
        }

        /// <overloads>
        /// <summary>
        /// Determines whether a value is less than another value (regarding a given tolerance).
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Determines whether a value is less than another value (regarding the tolerance 
        /// <see cref="EpsilonF"/>).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> &lt; <paramref name="value2"/> and the
        /// difference between <paramref name="value1"/> and <paramref name="value2"/> is greater than
        /// or equal to the epsilon tolerance; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsLess(float value1, float value2)
        {
            return (value1 < value2) && !AreEqual(value1, value2);
        }

        /// <summary>
        /// Determines whether a value is less than another value (regarding the specified tolerance).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> &lt; <paramref name="value2"/> and the
        /// difference between <paramref name="value1"/> and <paramref name="value2"/> is greater than
        /// or equal to the epsilon tolerance; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="epsilon"/> is negative or 0.
        /// </exception>
        public static bool IsLess(float value1, float value2, float epsilon)
        {
            return (value1 < value2) && !AreEqual(value1, value2, epsilon);
        }

        /// <summary>
        /// Determines whether a value is less than another value (regarding the tolerance 
        /// <see cref="EpsilonD"/>).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> &lt; <paramref name="value2"/> and the
        /// difference between <paramref name="value1"/> and <paramref name="value2"/> is greater than
        /// or equal to the epsilon tolerance; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsLess(double value1, double value2)
        {
            return (value1 < value2) && !AreEqual(value1, value2);
        }

        /// <summary>
        /// Determines whether a value is less than another value (regarding the specified tolerance).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> &lt; <paramref name="value2"/> and the
        /// difference between <paramref name="value1"/> and <paramref name="value2"/> is greater than
        /// or equal to the epsilon tolerance; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="epsilon"/> is negative or 0.
        /// </exception>
        public static bool IsLess(double value1, double value2, double epsilon)
        {
            return (value1 < value2) && !AreEqual(value1, value2, epsilon);
        }

        /// <overloads>
        /// <summary>
        /// Determines whether a value is less than or equal to another value (regarding a given 
        /// tolerance).
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Determines whether a value is less than or equal to another value (regarding the tolerance 
        /// <see cref="EpsilonF"/>).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> ≤ <paramref name="value2"/> or their 
        /// difference is less than the epsilon tolerance; otherwise, <see langword="false"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LessOr")]
        public static bool IsLessOrEqual(float value1, float value2)
        {
            return (value1 < value2) || AreEqual(value1, value2);
        }

        /// <summary>
        /// Determines whether a value is less than or equal to another value (regarding the specified
        /// tolerance).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> ≤ <paramref name="value2"/> or their 
        /// difference is less than the epsilon tolerance; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="epsilon"/> is negative or 0.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LessOr")]
        public static bool IsLessOrEqual(float value1, float value2, float epsilon)
        {
            return (value1 < value2) || AreEqual(value1, value2, epsilon);
        }

        /// <summary>
        /// Determines whether a value is less than or equal to another value (regarding the tolerance 
        /// <see cref="EpsilonD"/>).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> ≤ <paramref name="value2"/> or their 
        /// difference is less than the epsilon tolerance; otherwise, <see langword="false"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LessOr")]
        public static bool IsLessOrEqual(double value1, double value2)
        {
            return (value1 < value2) || AreEqual(value1, value2);
        }

        /// <summary>
        /// Determines whether a value is less than or equal to another value (regarding the specified 
        /// tolerance).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> ≤ <paramref name="value2"/> or their 
        /// difference is less than the epsilon tolerance; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="epsilon"/> is negative or 0.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "LessOr")]
        public static bool IsLessOrEqual(double value1, double value2, double epsilon)
        {
            return (value1 < value2) || AreEqual(value1, value2, epsilon);
        }

        /// <overloads>
        /// <summary>
        /// Determines whether a value is greater than another value (regarding a given tolerance).
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Determines whether a value is greater than another value (regarding the tolerance 
        /// <see cref="EpsilonF"/>).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>
        /// <see langword="true"/> if the difference between <paramref name="value1"/> and 
        /// <paramref name="value2"/> is greater than or equal to the epsilon tolerance and 
        /// <paramref name="value1"/> &gt; <paramref name="value2"/>; otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        public static bool IsGreater(float value1, float value2)
        {
            return (value1 > value2) && !AreEqual(value1, value2);
        }

        /// <summary>
        /// Determines whether a value is greater than another value (regarding the specified
        /// tolerance).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> &gt; <paramref name="value2"/> and the
        /// difference between <paramref name="value1"/> and <paramref name="value2"/> is greater than
        /// or equal to the epsilon tolerance; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="epsilon"/> is negative or 0.
        /// </exception>
        public static bool IsGreater(float value1, float value2, float epsilon)
        {
            return (value1 > value2) && !AreEqual(value1, value2, epsilon);
        }

        /// <summary>
        /// Determines whether a value is greater than another value (regarding the tolerance 
        /// <see cref="EpsilonD"/>).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> &gt; <paramref name="value2"/> and the
        /// difference between <paramref name="value1"/> and <paramref name="value2"/> is greater than
        /// or equal to the epsilon tolerance; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsGreater(double value1, double value2)
        {
            return (value1 > value2) && !AreEqual(value1, value2);
        }

        /// <summary>
        /// Determines whether a value is greater than another value (regarding the specified
        /// tolerance).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> &gt; <paramref name="value2"/> and the
        /// difference between <paramref name="value1"/> and <paramref name="value2"/> is greater than
        /// or equal to the epsilon tolerance; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="epsilon"/> is negative or 0.
        /// </exception>
        public static bool IsGreater(double value1, double value2, double epsilon)
        {
            return (value1 > value2) && !AreEqual(value1, value2, epsilon);
        }

        /// <overloads>
        /// <summary>
        /// Determines whether a value is greater than or equal to another value (regarding a given 
        /// tolerance).
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Determines whether a value is greater than or equal to another value (regarding the
        /// tolerance <see cref="EpsilonF"/>).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> ≥ <paramref name="value2"/> or their 
        /// difference is less than the epsilon tolerance; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsGreaterOrEqual(float value1, float value2)
        {
            return (value1 > value2) || AreEqual(value1, value2);
        }

        /// <summary>
        /// Determines whether a value is greater than or equal to another value (regarding the 
        /// specified tolerance).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> ≥ <paramref name="value2"/> or their 
        /// difference is less than the epsilon tolerance; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="epsilon"/> is negative or 0.
        /// </exception>
        public static bool IsGreaterOrEqual(float value1, float value2, float epsilon)
        {
            return (value1 > value2) || AreEqual(value1, value2, epsilon);
        }

        /// <summary>
        /// Determines whether a value is greater than or equal to another value (regarding the
        /// tolerance <see cref="EpsilonD"/>).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> ≥ <paramref name="value2"/> or their 
        /// difference is less than the epsilon tolerance; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsGreaterOrEqual(double value1, double value2)
        {
            return (value1 > value2) || AreEqual(value1, value2);
        }

        /// <summary>
        /// Determines whether a value is greater than or equal to another value (regarding the 
        /// specified tolerance).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value1"/> ≥ <paramref name="value2"/> or their 
        /// difference is less than the epsilon tolerance; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="epsilon"/> is negative or 0.
        /// </exception>
        public static bool IsGreaterOrEqual(double value1, double value2, double epsilon)
        {
            return (value1 > value2) || AreEqual(value1, value2, epsilon);
        }

        /// <overloads>
        /// <summary>
        /// Clamps near-zero values to zero.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Clamps near-zero values to zero.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 0 if value is nearly zero (within the tolerance <see cref="Numeric.EpsilonF"/>) or the
        /// original value otherwise.
        /// </returns>
        public static float ClampToZero(float value)
        {
            return IsZero(value) ? 0 : value;
        }

        /// <summary>
        /// Clamps near-zero values to zero.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>
        /// 0 if the value is nearly zero (within the tolerance <paramref name="epsilon"/>) or the 
        /// original value otherwise.
        /// </returns>
        public static float ClampToZero(float value, float epsilon)
        {
            return IsZero(value, epsilon) ? 0 : value;
        }

        /// <summary>
        /// Clamps near-zero values to zero.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 0 if the value is nearly zero (within the tolerance <see cref="Numeric.EpsilonF"/>) 
        /// or the original value otherwise.
        /// </returns>
        public static double ClampToZero(double value)
        {
            return IsZero(value) ? 0 : value;
        }

        /// <summary>
        /// Clamps near-zero values to zero.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>
        /// 0 if the value is nearly zero (within the tolerance <paramref name="epsilon"/>) or the
        /// original value otherwise.
        /// </returns>
        public static double ClampToZero(double value, double epsilon)
        {
            return IsZero(value, epsilon) ? 0 : value;
        }

        /// <overloads>
        /// <summary>
        /// Determines whether a value is zero (regarding a given tolerance).
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Determines whether a <paramref name="value"/> is zero (regarding the tolerance 
        /// <see cref="EpsilonF"/>).
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified value is zero (within the tolerance); otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// A value is zero if |x| &lt; <see cref="EpsilonF"/>.
        /// </remarks>
        public static bool IsZero(float value)
        {
            return (-_epsilonF < value) && (value < _epsilonF);
        }

        /// <summary>
        /// Determines whether a value is zero (regarding the tolerance 
        /// <see cref="EpsilonD"/>).
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified value is zero (within the tolerance); otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// A value is zero if |x| &lt; <see cref="EpsilonD"/>.
        /// </remarks>
        public static bool IsZero(double value)
        {
            return (-_epsilonD < value) && (value < _epsilonD);
        }

        /// <summary>
        /// Determines whether a value is zero (regarding a specific tolerance).
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>
        /// <see langword="true"/> if the specified value is zero (within the tolerance); otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// A value is zero if |x| &lt; epsilon.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="epsilon"/> is negative or 0.
        /// </exception>
        public static bool IsZero(float value, float epsilon)
        {
            if (epsilon <= 0.0f)
                throw new ArgumentOutOfRangeException("epsilon", "Epsilon value must be greater than 0.");

            return (-epsilon < value) && (value < epsilon);
        }

        /// <summary>
        /// Determines whether a value is zero (regarding a specific tolerance).
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>
        /// <see langword="true"/> if the specified value is zero (within the tolerance); otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// A value is zero if |x| &lt; epsilon.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="epsilon"/> is negative or 0.
        /// </exception>
        public static bool IsZero(double value, double epsilon)
        {
            if (epsilon <= 0.0)
                throw new ArgumentOutOfRangeException("epsilon", "Epsilon value must be greater than 0.");

            return (-epsilon < value) && (value < epsilon);
        }

        /// <overloads>
        /// <summary>
        /// Compares two floating-point values (regarding a given tolerance).
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Compares two <see langword="float"/> values (regarding the tolerance 
        /// <see cref="EpsilonF"/>).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>
        /// -1 if <paramref name="value1"/> is less than <paramref name="value2"/>, +1 if 
        /// <paramref name="value1"/> is greater than <paramref name="value2"/>, and 0 if the values are
        /// equal (within the tolerance <see cref="EpsilonF"/>).
        /// </returns>
        /// <remarks>
        /// <strong>Important:</strong> When at least one of the parameters is a special floating-point
        /// value (such as <see cref="Single.NaN"/>, <see cref="Single.PositiveInfinity"/>, or 
        /// <see cref="Single.NegativeInfinity"/>), the result is undefined. Such cases should be
        /// handled explicitly by the calling application.
        /// </remarks>
        public static int Compare(float value1, float value2)
        {
            if (AreEqual(value1, value2))
                return 0;

            return (value1 < value2) ? -1 : +1;
        }

        /// <summary>
        /// Compares two <see langword="double"/> values (regarding the tolerance 
        /// <see cref="EpsilonD"/>).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>
        /// -1 if <paramref name="value1"/> is less than <paramref name="value2"/>, +1 if 
        /// <paramref name="value1"/> is greater than <paramref name="value2"/>, and 0 if the values are
        /// equal (within the tolerance <see cref="EpsilonF"/>).
        /// </returns>
        /// <remarks>
        /// <strong>Important:</strong> When at least one of the parameters is a special floating-point
        /// value (such as <see cref="Double.NaN"/>, <see cref="Double.PositiveInfinity"/>, or 
        /// <see cref="Double.NegativeInfinity"/>), the result is undefined. Such cases should be
        /// handled explicitly by the calling application.
        /// </remarks>
        public static int Compare(double value1, double value2)
        {
            if (AreEqual(value1, value2))
                return 0;

            return (value1 < value2) ? -1 : +1;
        }

        /// <summary>
        /// Compares two <see langword="float"/> values (regarding a specific tolerance).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="epsilon">The tolerance value for equality.</param>
        /// <returns>
        /// -1 if <paramref name="value1"/> is less than <paramref name="value2"/>, +1 if 
        /// <paramref name="value1"/> is greater than <paramref name="value2"/>, and 0 if the values are
        /// equal (within the tolerance <paramref name="epsilon"/>).
        /// </returns>
        /// <remarks>
        /// <strong>Important:</strong> When at least one of the parameters is a special floating-point
        /// value (such as <see cref="Single.NaN"/>, <see cref="Single.PositiveInfinity"/>, or 
        /// <see cref="Single.NegativeInfinity"/>), the result is undefined. Such cases should be
        /// handled explicitly by the calling application.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="epsilon"/> is negative or 0.
        /// </exception>
        public static int Compare(float value1, float value2, float epsilon)
        {
            if (AreEqual(value1, value2, epsilon))
                return 0;

            return (value1 < value2) ? -1 : +1;
        }

        /// <summary>
        /// Compares two <see langword="double"/> values (regarding a specific tolerance).
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="epsilon">The tolerance value for equality.</param>
        /// <returns>
        /// -1 if <paramref name="value1"/> is less than <paramref name="value2"/>, +1 if 
        /// <paramref name="value1"/> is greater than <paramref name="value2"/>, and 0 if the values are
        /// equal (within the tolerance <paramref name="epsilon"/>).
        /// </returns>
        /// <remarks>
        /// <strong>Important:</strong> When at least one of the parameters is a special 
        /// floating-point value (such as <see cref="Double.NaN"/>, <see cref="Double.PositiveInfinity"/>, 
        /// or <see cref="Double.NegativeInfinity"/>), the result is undefined. Such cases 
        /// should be handled explicitly by the calling application.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="epsilon"/> is negative or 0.
        /// </exception>
        public static int Compare(double value1, double value2, double epsilon)
        {
            if (AreEqual(value1, value2, epsilon))
                return 0;

            return (value1 < value2) ? -1 : +1;
        }

        /// <overloads>
        /// <summary>
        /// Determines whether the specified value is finite or NaN.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Determines whether the specified value is finite or <see cref="Single.NaN"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is finite or <see cref="Single.NaN"/>; 
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsFiniteOrNaN(float value)
        {
            return !Single.IsInfinity(value);
        }

        /// <summary>
        /// Determines whether the specified value is finite or <see cref="Double.NaN"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is finite or <see cref="Double.NaN"/>; 
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsFiniteOrNaN(double value)
        {
            return !Double.IsInfinity(value);
        }

        /// <overloads>
        /// <summary>
        /// Determines whether the specified value is finite.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Determines whether the specified value is finite.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is finite; otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        public static bool IsFinite(float value)
        {
            return !IsNaN(value) && !Single.IsInfinity(value);
        }

        /// <summary>
        /// Determines whether the specified value is finite.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is finite; otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        public static bool IsFinite(double value)
        {
            return !IsNaN(value) && !Double.IsInfinity(value);
        }

        /// <overloads>
        /// <summary>
        /// Determines whether the specified value is positive.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Determines whether the specified value is positive.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is positive; otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        public static bool IsPositive(float value)
        {
            return 0 < value;
        }

        /// <summary>
        /// Determines whether the specified value is positive.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is positive; otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        public static bool IsPositive(double value)
        {
            return 0 < value;
        }

        /// <overloads>
        /// <summary>
        /// Determines whether the specified value is negative.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Determines whether the specified value is negative.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is negative; otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        public static bool IsNegative(float value)
        {
            return value < 0;
        }

        /// <summary>
        /// Determines whether the specified value is negative.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is negative; otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        public static bool IsNegative(double value)
        {
            return value < 0;
        }

        /// <overloads>
        /// <summary>
        /// Determines whether the specified value is positive finite.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Determines whether the specified value is positive finite.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is a positive finite number; otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        public static bool IsPositiveFinite(float value)
        {
            return 0 < value && !Single.IsPositiveInfinity(value);
        }

        /// <summary>
        /// Determines whether the specified value is positive finite.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is a positive finite number; otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        public static bool IsPositiveFinite(double value)
        {
            return 0 < value && !Double.IsPositiveInfinity(value);
        }

        /// <overloads>
        /// <summary>
        /// Determines whether the specified value is negative finite.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Determines whether the specified value is negative finite.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is a negative finite number; otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        public static bool IsNegativeFinite(float value)
        {
            return value < 0 && !Single.IsNegativeInfinity(value);
        }

        /// <summary>
        /// Determines whether the specified value is negative finite.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is a negative finite number; otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        public static bool IsNegativeFinite(double value)
        {
            return value < 0 && !Double.IsNegativeInfinity(value);
        }

        /// <overloads>
        /// <summary>
        /// Determines whether the specified value is 0 or positive finite.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Determines whether the specified value is 0 or positive finite.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is 0 or a positive finite number; 
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsZeroOrPositiveFinite(float value)
        {
            return 0 <= value && !Single.IsPositiveInfinity(value);
        }

        /// <summary>
        /// Determines whether the specified value is 0 or positive finite.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is 0 or a positive finite number; 
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsZeroOrPositiveFinite(double value)
        {
            return 0 <= value && !Double.IsPositiveInfinity(value);
        }

        /// <overloads>
        /// <summary>
        /// Determines whether the specified value is 0 or negative finite.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Determines whether the specified value is 0 or negative finite.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is 0 or a negative finite number; 
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsZeroOrNegativeFinite(float value)
        {
            return value <= 0 && !Single.IsNegativeInfinity(value);
        }

        /// <summary>
        /// Determines whether the specified value is 0 or negative finite.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is 0 or a negative finite number; 
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsZeroOrNegativeFinite(double value)
        {
            return value <= 0 && !Double.IsNegativeInfinity(value);
        }

        // ReSharper disable FieldCanBeMadeReadOnly.Local
        [StructLayout(LayoutKind.Explicit)]
        private struct SingleToUInt32
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
            [FieldOffset(0)]
            internal float Single;
            [FieldOffset(0)]
            internal UInt32 UInt32;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct DoubleToUInt64
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
            [FieldOffset(0)]
            internal double Double;
            [FieldOffset(0)]
            internal UInt64 UInt64;
        }
        // ReSharper restore FieldCanBeMadeReadOnly.Local

        /// <summary>
        /// Returns a value indicating whether the specified number is <see cref="float.NaN"/>.
        /// </summary>
        /// <param name="value">A single-precision floating-point number.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> evaluates to <see cref="float.NaN"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// The standard CLR <see cref="float.IsNaN"/> function is slower than this wrapper, so please
        /// make sure to use this <see cref="IsNaN(float)"/> in performance sensitive code.
        /// </remarks>
        public static bool IsNaN(float value)
        {
            // IEEE 754: 
            //   msb means most significant bit
            //   lsb means least significant bit
            //    1    8              23             ... widths
            //   +-+-------+-----------------------+
            //   |s|  exp  |          man          |
            //   +-+-------+-----------------------+
            //      msb lsb msb                 lsb  ... order
            //  
            //  If exp = 255 and man != 0, then value is NaN regardless of s.
            //
            // => If the argument is any value in the range 0x7f800001 through 0x7fffffff or in the range 
            // 0xff800001 through 0xffffffff, the result will be NaN.
            SingleToUInt32 t = new SingleToUInt32 { Single = value };

            UInt32 exp = t.UInt32 & 0x7f800000;
            UInt32 man = t.UInt32 & 0x007fffff;

            return exp == 0x7f800000 && man != 0;
        }

        /// <summary>
        /// Returns a value indicating whether the specified number is <see cref="double.NaN"/>.
        /// </summary>
        /// <param name="value">A double-precision floating-point number.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> evaluates to <see cref="double.NaN"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// The standard CLR <see cref="double.IsNaN"/> function is slower than this wrapper, so please
        /// make sure to use this <see cref="IsNaN(double)"/> in performance sensitive code.
        /// </remarks>
        public static bool IsNaN(double value)
        {
            // IEEE 754: 
            //   msb means most significant bit
            //   lsb means least significant bit
            //    1   11              52             ... widths
            //   +-+-------+-----------------------+
            //   |s|  exp  |          man          |
            //   +-+-------+-----------------------+
            //      msb lsb msb                 lsb  ... order
            //  
            //  If exp = 2047 and man != 0, then value is NaN regardless of s.
            //
            // => If the argument is any value in the range 0x7ff0000000000001L through 
            // 0x7fffffffffffffffL or in the range 0xfff0000000000001L through 0xffffffffffffffffL, the 
            // result will be NaN.
            DoubleToUInt64 t = new DoubleToUInt64 { Double = value };

            UInt64 exp = t.UInt64 & 0x7ff0000000000000;
            UInt64 man = t.UInt64 & 0x000fffffffffffff;

            return exp == 0x7ff0000000000000 && man != 0;
        }

        /// <summary>
        /// Gets the significant bits of a floating-point number, which can be used for rough 
        /// comparisons or sorting. (The floating-point number must be positive.)
        /// </summary>
        /// <param name="value">The floating-point number.</param>
        /// <param name="n">The number of significant bits.</param>
        /// <returns>The <paramref name="n"/> significant bits of <paramref name="value"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "unsigned")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "n")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "31-n")]
        [CLSCompliant(false)]
        public static uint GetSignificantBitsUnsigned(float value, int n)
        {
            // Reference: http://aras-p.info/blog/2014/01/16/rough-sorting-by-depth/

            // Example:
            // Taking highest 9 bits for rough sort for positive floats.
            //   0.01 maps to 240,
            //   0.1 maps to 247, 
            //   1.0 maps to 254,
            //   10.0 maps to 260,
            //   100.0 maps to 267,
            //   1000.0 maps to 273, etc.
            SingleToUInt32 t = new SingleToUInt32 { Single = value };
            uint b = t.UInt32 >> (31 - n);  // Take highest n bits.
            return b;
        }

        /// <summary>
        /// Gets the significant bits of a floating-point number, which can be used for rough
        /// comparisons or sorting. (The floating-point number can negative or positive.)
        /// </summary>
        /// <param name="value">The floating-point number.</param>
        /// <param name="n">The number of significant bits.</param>
        /// <returns>The <paramref name="n"/> significant bits of <paramref name="value"/>.</returns>
        /// <remarks>
        /// The sign bit is flipped to ensure that the bit representation of a positive floating-point
        /// number is greater than the bit representation of a negative number.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "signed")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "n")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "32-n")]
        [CLSCompliant(false)]
        public static uint GetSignificantBitsSigned(float value, int n)
        {
            // Example:
            // Taking highest 10 bits for rough sort for positive floats.
            //   0.01 maps to 752,
            //   0.1 maps to 759,
            //   1.0 maps to 766,
            //   10.0 maps to 772,
            //   100.0 maps to 779, etc.
            // Negative numbers go similarly in 0 ... 511 range.
            SingleToUInt32 t = new SingleToUInt32 { Single = value };
            t.UInt32 = FloatFlip(t.UInt32);   // Flip bits to be sortable.
            uint b = t.UInt32 >> (32 - n);    // Take highest n bits.
            return b;
        }

        private static uint FloatFlip(uint f)
        {
            uint mask = (uint)(-(int)(f >> 31)) | 0x80000000u;
            return f ^ mask;
        }
    }
}

