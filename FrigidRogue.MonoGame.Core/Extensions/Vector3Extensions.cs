using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;

using FrigidRogue.MonoGame.Core.Components;

using Microsoft.Xna.Framework;

using MathHelper = FrigidRogue.MonoGame.Core.Components.MathHelper;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 Truncate(this Vector3 vector3, float limit)
        {
            if (vector3.LengthSquared() > limit * limit)
                return Vector3.Normalize(vector3) * limit;

            return vector3;
        }

        public static float Index(this Vector3 vector, int index)
        {
            switch (index)
            {
                case 0: return vector.X;
                case 1: return vector.Y;
                case 2: return vector.Z;
                default: throw new ArgumentOutOfRangeException("index", "The index is out of range. Allowed values are 0, 1, or 2.");
            }
        }

        public static void SetIndex(this Vector3 vector, int index, float value)
        {
            switch (index)
            {
                case 0:
                    vector.X = value;
                    break;
                case 1:
                    vector.Y = value;
                    break;
                case 2:
                    vector.Z = value;
                    break;
                default: throw new ArgumentOutOfRangeException("index", "The index is out of range. Allowed values are 0, 1, or 2.");
            }
        }

        /// <summary>
        /// Gets a value indicating whether a component of the vector is <see cref="float.NaN"/>.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if a component of the vector is <see cref="float.NaN"/>; otherwise, 
        /// <see langword="false"/>.
        /// </value>
        public static bool IsNaN(this Vector3 vector)
        {
            return float.IsNaN(vector.X)
                   || float.IsNaN(vector.Y)
                   || float.IsNaN(vector.Z);
        }

        /// <summary>
        /// Returns a value indicating whether this vector is normalized (the length is numerically
        /// equal to 1).
        /// </summary>
        /// <value>
        /// <see langword="true"/> if this <see cref="Vector3"/> is normalized; otherwise, 
        /// <see langword="false"/>.
        /// </value>
        /// <remarks>
        /// <see cref="IsNumericallyNormalized"/> compares the length of this vector against 1.0 using
        /// the default tolerance value (see <see cref="Numeric.EpsilonF"/>).
        /// </remarks>
        public static bool IsNumericallyNormalized(this Vector3 vector)
        {
            return Numeric.AreEqual(vector.LengthSquared(), 1.0f);
        }

        /// <summary>
        /// Returns a value indicating whether this vector has zero size (the length is numerically
        /// equal to 0).
        /// </summary>
        /// <value>
        /// <see langword="true"/> if this vector is numerically zero; otherwise, 
        /// <see langword="false"/>.
        /// </value>
        /// <remarks>
        /// The length of this vector is compared to 0 using the default tolerance value (see 
        /// <see cref="Numeric.EpsilonF"/>).
        /// </remarks>
        public static bool IsNumericallyZero(this Vector3 vector)
        {
            return Numeric.IsZero(vector.LengthSquared(), Numeric.EpsilonFSquared);
        }

        public static void SetLength(this Vector3 vector, float value)
        {
            float length = vector.Length();

            if (Numeric.IsZero(length))
                throw new Exception("Cannot change length of a vector with length 0.");

            float scale = value / length;

            vector.X *= scale;
            vector.Y *= scale;
            vector.Z *= scale;
        }

        /// <summary>
        /// Returns an arbitrary normalized <see cref="Vector3"/> that is orthogonal to this vector.
        /// </summary>
        /// <value>An arbitrary normalized orthogonal <see cref="Vector3"/>.</value>
        public static Vector3 Orthonormal1(this Vector3 vector)
        {
            // Note: Other options to create normal vectors are discussed here:
            // http://blog.selfshadow.com/2011/10/17/perp-vectors/,
            // http://box2d.org/2014/02/computing-a-basis/
            // and here
            // "Building an Orthonormal Basis from a 3D Unit Vector Without Normalization"
            // http://orbit.dtu.dk/fedora/objects/orbit:113874/datastreams/file_75b66578-222e-4c7d-abdf-f7e255100209/content
            // This method is implemented in DigitalRune.Graphics/Misc.fxh/GetOrthonormals().
            Vector3 v;
            if (Numeric.IsZero(vector.Z) == false)
            {
                // Orthonormal = (1, 0, 0) x (X, Y, Z)
                v.X = 0f;
                v.Y = -vector.Z;
                v.Z = vector.Y;
            }
            else
            {
                // Orthonormal = (0, 0, 1) x (X, Y, Z)
                v.X = -vector.Y;
                v.Y = vector.X;
                v.Z = 0f;
            }

            v.Normalize();
            return v;
        }

        /// <summary>
        /// Gets a normalized orthogonal <see cref="Vector3"/> that is orthogonal to this 
        /// <see cref="Vector3"/> and to <see cref="Orthonormal1"/>.
        /// </summary>
        /// <value>
        /// A normalized orthogonal <see cref="Vector3"/> which is orthogonal to this 
        /// <see cref="Vector3"/> and to <see cref="Orthonormal1"/>.
        /// </value>
        public static Vector3 Orthonormal2(this Vector3 vector)
        {
            Vector3 v = Vector3.Cross(vector, Orthonormal1(vector));
            v.Normalize();
            return v;
        }

        /// <summary>
        /// Gets the value of the largest component.
        /// </summary>
        /// <value>The value of the largest component.</value>
        public static float LargestComponent(this Vector3 vector)
        {
            if (vector.X >= vector.Y && vector.X >= vector.Z)
                return vector.X;

            if (vector.Y >= vector.Z)
                return vector.Y;

            return vector.Z;
        }

        /// <summary>
        /// Gets the index (zero-based) of the largest component.
        /// </summary>
        /// <value>The index (zero-based) of the largest component.</value>
        /// <remarks>
        /// <para>
        /// This method returns the index of the component (X, Y or Z) which has the largest value. The 
        /// index is zero-based, i.e. the index of X is 0. 
        /// </para>
        /// <para>
        /// If there are several components with equally large values, the smallest index of these is 
        /// returned.
        /// </para>
        /// </remarks>
        public static int IndexOfLargestComponent(this Vector3 vector)
        {
            if (vector.X >= vector.Y && vector.X >= vector.Z)
                return 0;

            if (vector.Y >= vector.Z)
                return 1;

            return 2;
        }

        /// <summary>
        /// Gets the value of the smallest component.
        /// </summary>
        /// <value>The value of the smallest component.</value>
        public static float SmallestComponent(this Vector3 vector)
        {
            if (vector.X <= vector.Y && vector.X <= vector.Z)
                return vector.X;

            if (vector.Y <= vector.Z)
                return vector.Y;

            return vector.Z;
        }

        /// <summary>
        /// Gets the index (zero-based) of the largest component.
        /// </summary>
        /// <value>The index (zero-based) of the largest component.</value>
        /// <remarks>
        /// <para>
        /// This method returns the index of the component (X, Y or Z) which has the smallest value. The 
        /// index is zero-based, i.e. the index of X is 0. 
        /// </para>
        /// <para>
        /// If there are several components with equally small values, the smallest index of these is 
        /// returned.
        /// </para>
        /// </remarks>
        public static int IndexOfSmallestComponent(this Vector3 vector)
        {
            if (vector.X <= vector.Y && vector.X <= vector.Z)
                return 0;

            if (vector.Y <= vector.Z)
                return 1;

            return 2;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Vector3"/>.
        /// </summary>
        /// <param name="components">
        /// Array with the initial values for the components x, y and z.
        /// </param>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="components"/> has less than 3 elements.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// <paramref name="components"/> must not be <see langword="null"/>.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods")]
        public static Vector3 FromComponents(this float[] components)
        {
            return new Vector3(
                components[0],
                components[1],
                components[2]);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> class.
        /// </summary>
        /// <param name="components">
        /// List with the initial values for the components x, y and z.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="components"/> has less than 3 elements.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// <paramref name="components"/> must not be <see langword="null"/>.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods")]
        public static Vector3 FromComponents(this IList<float> components)
        {
            return new Vector3(
                components[0],
                components[1],
                components[2]);
        }

        /// <summary>
        /// Tests if each component of a vector is greater than the corresponding component of another
        /// vector.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>
        /// <see langword="true"/> if each component of <paramref name="vector1"/> is greater than its
        /// counterpart in <paramref name="vector2"/>; otherwise, <see langword="false"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
        public static bool GreaterThan(this Vector3 vector1, Vector3 vector2)
        {
            return vector1.X > vector2.X
                   && vector1.Y > vector2.Y
                   && vector1.Z > vector2.Z;
        }

        /// <summary>
        /// Tests if each component of a vector is greater or equal than the corresponding component of
        /// another vector.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>
        /// <see langword="true"/> if each component of <paramref name="vector1"/> is greater or equal
        /// than its counterpart in <paramref name="vector2"/>; otherwise, <see langword="false"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
        public static bool GreaterThanOrEqualTo(this Vector3 vector1, Vector3 vector2)
        {
            return vector1.X >= vector2.X
                   && vector1.Y >= vector2.Y
                   && vector1.Z >= vector2.Z;
        }

        /// <summary>
        /// Tests if each component of a vector is less than the corresponding component of another
        /// vector.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>
        /// <see langword="true"/> if each component of <paramref name="vector1"/> is less than its 
        /// counterpart in <paramref name="vector2"/>; otherwise, <see langword="false"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
        public static bool LessThan(this Vector3 vector1, Vector3 vector2)
        {
            return vector1.X < vector2.X
                   && vector1.Y < vector2.Y
                   && vector1.Z < vector2.Z;
        }

        /// <summary>
        /// Tests if each component of a vector is less or equal than the corresponding component of
        /// another vector.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>
        /// <see langword="true"/> if each component of <paramref name="vector1"/> is less or equal than
        /// its counterpart in <paramref name="vector2"/>; otherwise, <see langword="false"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
        public static bool LessThanOrEqualTo(this Vector3 vector1, Vector3 vector2)
        {
            return vector1.X <= vector2.X
                   && vector1.Y <= vector2.Y
                   && vector1.Z <= vector2.Z;
        }

        /// <overloads>
        /// <summary>
        /// Converts a vector to another data type.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Converts a vector to an array of 3 <see langword="float"/> values.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>
        /// The array with 3 <see langword="float"/> values. The order of the elements is: x, y, z
        /// </returns>
        public static float[] ToArray(this Vector3 vector)
        {
            return new[] { vector.X, vector.Y, vector.Z };
        }

        /// <summary>
        /// Converts a vector to a list of 3 <see langword="float"/> values.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The result of the conversion. The order of the elements is: x, y, z</returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static List<float> ToList(this Vector3 vector)
        {
            List<float> result = new List<float>(3) { vector.X, vector.Y, vector.Z };
            return result;
        }

        /// <summary>
        /// Tries to normalize the vector.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the vector was normalized; otherwise, <see langword="false"/> if 
        /// the vector could not be normalized. (The length is numerically zero.)
        /// </returns>
        public static bool TryNormalize(this Vector3 vector, out Vector3 result)
        {
            result = Vector3.Zero;
            float lengthSquared = vector.LengthSquared();
            if (Numeric.IsZero(lengthSquared, Numeric.EpsilonFSquared))
                return false;

            float length = (float)Math.Sqrt(lengthSquared);

            float scale = 1.0f / length;


            result.X *= scale;
            result.Y *= scale;
            result.Z *= scale;

            return true;
        }

        /// <summary>
        /// Returns the cross product matrix (skew matrix) of this vector.
        /// </summary>
        /// <returns>The cross product matrix of this vector.</returns>
        /// <remarks>
        /// <c>Vector3.Cross(v, w)</c> is the same as <c>v.ToCrossProductMatrix() * w</c>.
        /// </remarks>
        public static Matrix ToCrossProductMatrix(this Vector3 vector)
        {
            return new Matrix(
                0, -vector.Z, vector.Y, 0,
                vector.Z, 0, -vector.X, 0,
                -vector.Y, vector.X, 0, 0,
                0, 0, 0, 1);
        }

        /// <summary>
        /// Returns a vector with the absolute values of the elements of the given vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>A vector with the absolute values of the elements of the given vector.</returns>
        public static Vector3 Absolute(this Vector3 vector)
        {
            return new Vector3(Math.Abs(vector.X), Math.Abs(vector.Y), Math.Abs(vector.Z));
        }

        /// <overloads>
        /// <summary>
        /// Determines whether two vectors are equal (regarding a given tolerance).
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Determines whether two vectors are equal (regarding the tolerance 
        /// <see cref="Numeric.EpsilonF"/>).
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>
        /// <see langword="true"/> if the vectors are equal (within the tolerance 
        /// <see cref="Numeric.EpsilonF"/>); otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// The two vectors are compared component-wise. If the differences of the components are less
        /// than <see cref="Numeric.EpsilonF"/> the vectors are considered as being equal.
        /// </remarks>
        public static bool AreNumericallyEqual(this Vector3 vector1, Vector3 vector2)
        {
            return Numeric.AreEqual(vector1.X, vector2.X)
                   && Numeric.AreEqual(vector1.Y, vector2.Y)
                   && Numeric.AreEqual(vector1.Z, vector2.Z);
        }

        /// <summary>
        /// Determines whether two vectors are equal (regarding a specific tolerance).
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>
        /// <see langword="true"/> if the vectors are equal (within the tolerance 
        /// <paramref name="epsilon"/>); otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// The two vectors are compared component-wise. If the differences of the components are less
        /// than <paramref name="epsilon"/> the vectors are considered as being equal.
        /// </remarks>
        public static bool AreNumericallyEqual(this Vector3 vector1, Vector3 vector2, float epsilon)
        {
            return Numeric.AreEqual(vector1.X, vector2.X, epsilon)
                   && Numeric.AreEqual(vector1.Y, vector2.Y, epsilon)
                   && Numeric.AreEqual(vector1.Z, vector2.Z, epsilon);
        }

        /// <summary>
        /// Returns a vector with near-zero vector components clamped to 0.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The vector with small components clamped to zero.</returns>
        /// <remarks>
        /// Each vector component (X, Y and Z) is compared to zero. If the component is in the interval 
        /// [-<see cref="Numeric.EpsilonF"/>, +<see cref="Numeric.EpsilonF"/>] it is set to zero, 
        /// otherwise it remains unchanged.
        /// </remarks>
        public static Vector3 ClampToZero(this Vector3 vector)
        {
            vector.X = Numeric.ClampToZero(vector.X);
            vector.Y = Numeric.ClampToZero(vector.Y);
            vector.Z = Numeric.ClampToZero(vector.Z);
            return vector;
        }

        /// <summary>
        /// Returns a vector with near-zero vector components clamped to 0.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>The vector with small components clamped to zero.</returns>
        /// <remarks>
        /// Each vector component (X, Y and Z) is compared to zero. If the component is in the interval 
        /// [-<paramref name="epsilon"/>, +<paramref name="epsilon"/>] it is set to zero, otherwise it 
        /// remains unchanged.
        /// </remarks>
        public static Vector3 ClampToZero(this Vector3 vector, float epsilon)
        {
            vector.X = Numeric.ClampToZero(vector.X, epsilon);
            vector.Y = Numeric.ClampToZero(vector.Y, epsilon);
            vector.Z = Numeric.ClampToZero(vector.Z, epsilon);
            return vector;
        }

        /// <summary>
        /// Calculates the angle between two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The angle between the given vectors, such that 0 ≤ angle ≤ π.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="vector1"/> or <paramref name="vector2"/> has a length of 0.
        /// </exception>
        public static float GetAngle(this Vector3 vector1, Vector3 vector2)
        {
            if (!vector1.TryNormalize(out _) || !vector2.TryNormalize(out _))
                throw new ArgumentException("vector1 and vector2 must not have 0 length.");

            float α = Vector3.Dot(vector1, vector2);

            // Inaccuracy in the floating-point operations can cause
            // the result be outside of the valid range.
            // Ensure that the dot product α lies in the interval [-1, 1].
            // Math.Acos() returns Double.NaN if the argument lies outside
            // of this interval.
            α = MathHelper.Clamp(α, -1.0f, 1.0f);

            return (float)Math.Acos(α);
        }

        /// <summary>
        /// Returns a vector that contains the lowest value from each matching pair of components.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The minimized vector.</returns>
        public static Vector3 Min(this Vector3 vector1, Vector3 vector2)
        {
            vector1.X = Math.Min(vector1.X, vector2.X);
            vector1.Y = Math.Min(vector1.Y, vector2.Y);
            vector1.Z = Math.Min(vector1.Z, vector2.Z);
            return vector1;
        }

        /// <summary>
        /// Returns a vector that contains the highest value from each matching pair of components.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The maximized vector.</returns>
        public static Vector3 Max(this Vector3 vector1, Vector3 vector2)
        {
            vector1.X = Math.Max(vector1.X, vector2.X);
            vector1.Y = Math.Max(vector1.Y, vector2.Y);
            vector1.Z = Math.Max(vector1.Z, vector2.Z);
            return vector1;
        }

        /// <summary>
        /// Projects a vector onto an axis given by the target vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="target">The target vector.</param>
        /// <returns>
        /// The projection of <paramref name="vector"/> onto <paramref name="target"/>.
        /// </returns>
        public static Vector3 ProjectTo(this Vector3 vector, Vector3 target)
        {
            return Vector3.Dot(vector, target) / target.LengthSquared() * target;
        }

        /// <overloads>
        /// <summary>
        /// Converts the string representation of a 3-dimensional vector to its <see cref="Vector3"/>
        /// equivalent.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Converts the string representation of a 3-dimensional vector to its <see cref="Vector3"/>
        /// equivalent.
        /// </summary>
        /// <param name="s">A string representation of a 3-dimensional vector.</param>
        /// <returns>
        /// A <see cref="Vector3"/> that represents the vector specified by the <paramref name="s"/>
        /// parameter.
        /// </returns>
        /// <remarks>
        /// This version of <see cref="Parse(string)"/> uses the <see cref="CultureInfo"/> associated
        /// with the current thread.
        /// </remarks>
        /// <exception cref="FormatException">
        /// <paramref name="s"/> is not a valid <see cref="Vector3"/>.
        /// </exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static Vector3 Parse(string s)
        {
            return Parse(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the string representation of a 3-dimensional vector in a specified culture-specific
        /// format to its <see cref="Vector3"/> equivalent.
        /// </summary>
        /// <param name="s">A string representation of a 3-dimensional vector.</param>
        /// <param name="provider">
        /// An <see cref="IFormatProvider"/> that supplies culture-specific formatting information about
        /// <paramref name="s"/>. 
        /// </param>
        /// <returns>
        /// A <see cref="Vector3"/> that represents the vector specified by the <paramref name="s"/>
        /// parameter.
        /// </returns>
        /// <exception cref="FormatException">
        /// <paramref name="s"/> is not a valid <see cref="Vector3"/>.
        /// </exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static Vector3 Parse(string s, IFormatProvider provider)
        {
            Match m = Regex.Match(s, @"\((?<x>.*);(?<y>.*);(?<z>.*)\)", RegexOptions.None);
            if (m.Success)
            {
                return new Vector3(
                    float.Parse(m.Groups["x"].Value, provider),
                    float.Parse(m.Groups["y"].Value, provider),
                    float.Parse(m.Groups["z"].Value, provider));
            }

            throw new FormatException("String is not a valid Vector3.");
        }
    }
}