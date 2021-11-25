using System;

using FrigidRogue.MonoGame.Core.Components;

using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class MatrixExtensions
    {
        public static Matrix SetScaleOne(this Matrix matrix)
        {
            return matrix.SetScale(Vector3.One);
        }
        
        public static Matrix SetScale(this Matrix matrix, Vector3 scale)
        {
            var matrixNew = new Matrix(
                scale.X, matrix.M12, matrix.M13, matrix.M14,
                matrix.M21, scale.Y, matrix.M23, matrix.M24,
                matrix.M31, matrix.M32, scale.Z, matrix.M34,
                matrix.M41, matrix.M42, matrix.M43, matrix.M44);

            return matrixNew;
        }

        public static Vector3 Scale(this Matrix matrix)
        {
            var vector3 = new Vector3(matrix.M11, matrix.M22, matrix.M33);

            return vector3;
        }

        public static Vector4 Multiply(this Matrix matrix, Vector4 vector)
        {
            var product = new Vector4();

            for (var i = 0; i < 4; i++)
                for (var k = 0; k < 4; k++)
                    product.SetIndex(i, product.Index(i) + matrix[i, k] * vector.Index(k));

            return product;
        }

        public static Vector3 Multiply(this Matrix matrix, Vector3 vector)
        {
            var product = new Vector3();

            for (var i = 0; i < 3; i++)
                for (var k = 0; k < 3; k++)
                    product.SetIndex(i, product.Index(i) + matrix[i, k] * vector.Index(k));

            return product;
        }

        /// <overloads>
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <value>The element at <paramref name="index"/>.</value>
        /// <remarks>
        /// The matrix elements are in row-major order.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="index"/> is out of range.
        /// </exception>
        public static float GetIndex(this Matrix matrix, int index)
        {
            switch (index)
            {
                case 0: return matrix.M11;
                case 1: return matrix.M21;
                case 2: return matrix.M31;
                case 3: return matrix.M41;
                case 4: return matrix.M12;
                case 5: return matrix.M22;
                case 6: return matrix.M32;
                case 7: return matrix.M42;
                case 8: return matrix.M13;
                case 9: return matrix.M23;
                case 10: return matrix.M33;
                case 11: return matrix.M43;
                case 12: return matrix.M14;
                case 13: return matrix.M24;
                case 14: return matrix.M34;
                case 15: return matrix.M44;
                default:
                    throw new ArgumentOutOfRangeException("index", "The index is out of range. Allowed values are 0 to 15.");
            }
        }

        public static void SetIndex(this Matrix matrix, int index, float value)
        {
            switch (index)
            {
                case 0:
                    matrix.M11 = value;
                    break;
                case 1:
                    matrix.M21 = value;
                    break;
                case 2:
                    matrix.M31 = value;
                    break;
                case 3:
                    matrix.M41 = value;
                    break;
                case 4:
                    matrix.M12 = value;
                    break;
                case 5:
                    matrix.M22 = value;
                    break;
                case 6:
                    matrix.M32 = value;
                    break;
                case 7:
                    matrix.M42 = value;
                    break;
                case 8:
                    matrix.M13 = value;
                    break;
                case 9:
                    matrix.M23 = value;
                    break;
                case 10:
                    matrix.M33 = value;
                    break;
                case 11:
                    matrix.M43 = value;
                    break;
                case 12:
                    matrix.M14 = value;
                    break;
                case 13:
                    matrix.M24 = value;
                    break;
                case 14:
                    matrix.M34 = value;
                    break;
                case 15:
                    matrix.M44 = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("index", "The index is out of range. Allowed values are 0 to 15.");
            }
        }


        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="column">The column index.</param>
        /// <value>The element at the specified row and column.</value>
        /// <remarks>
        /// The indices are zero-based: [0,0] is the first element, [3,3] is the last element.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The index [<paramref name="row"/>, <paramref name="column"/>] is out of range.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1023:IndexersShouldNotBeMultidimensional")]
        public static float GetIndex(this Matrix matrix, int row, int column)
        {
            switch (row)
            {
                case 0:
                    switch (column)
                    {
                        case 0: return matrix.M11;
                        case 1: return matrix.M21;
                        case 2: return matrix.M31;
                        case 3: return matrix.M41;
                        default:
                            throw new ArgumentOutOfRangeException("column", "The column index is out of range. Allowed values are 0 to 3.");
                    }
                case 1:
                    switch (column)
                    {
                        case 0: return matrix.M12;
                        case 1: return matrix.M22;
                        case 2: return matrix.M32;
                        case 3: return matrix.M42;
                        default:
                            throw new ArgumentOutOfRangeException("column", "The column index is out of range. Allowed values are 0 to 3.");
                    }
                case 2:
                    switch (column)
                    {
                        case 0: return matrix.M13;
                        case 1: return matrix.M23;
                        case 2: return matrix.M33;
                        case 3: return matrix.M43;
                        default:
                            throw new ArgumentOutOfRangeException("column", "The column index is out of range. Allowed values are 0 to 3.");
                    }
                case 3:
                    switch (column)
                    {
                        case 0: return matrix.M14;
                        case 1: return matrix.M24;
                        case 2: return matrix.M34;
                        case 3: return matrix.M44;
                        default:
                            throw new ArgumentOutOfRangeException("column", "The column index is out of range. Allowed values are 0 to 3.");
                    }
                default:
                    throw new ArgumentOutOfRangeException("row", "The row index is out of range. Allowed values are 0 to 3.");
            }
        }

        public static void SetIndex(this Matrix matrix, int row, int column, float value)
        {
            switch (row)
            {
                case 0:
                    switch (column)
                    {
                        case 0:
                            matrix.M11 = value;
                            break;
                        case 1:
                            matrix.M21 = value;
                            break;
                        case 2:
                            matrix.M31 = value;
                            break;
                        case 3:
                            matrix.M41 = value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("column", "The column index is out of range. Allowed values are 0 to 3.");
                    }

                    break;
                case 1:
                    switch (column)
                    {
                        case 0:
                            matrix.M12 = value;
                            break;
                        case 1:
                            matrix.M22 = value;
                            break;
                        case 2:
                            matrix.M32 = value;
                            break;
                        case 3:
                            matrix.M42 = value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("column", "The column index is out of range. Allowed values are 0 to 3.");
                    }

                    break;
                case 2:
                    switch (column)
                    {
                        case 0:
                            matrix.M13 = value;
                            break;
                        case 1:
                            matrix.M23 = value;
                            break;
                        case 2:
                            matrix.M33 = value;
                            break;
                        case 3:
                            matrix.M43 = value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("column", "The column index is out of range. Allowed values are 0 to 3.");
                    }

                    break;
                case 3:
                    switch (column)
                    {
                        case 0:
                            matrix.M14 = value;
                            break;
                        case 1:
                            matrix.M24 = value;
                            break;
                        case 2:
                            matrix.M34 = value;
                            break;
                        case 3:
                            matrix.M44 = value;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("column", "The column index is out of range. Allowed values are 0 to 3.");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException("row", "The row index is out of range. Allowed values are 0 to 3.");
            }
        }


        /// <summary>
        /// Gets a value indicating whether an element of the matrix is <see cref="float.NaN"/>.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if an element of the matrix is <see cref="float.NaN"/>; otherwise, 
        /// <see langword="false"/>.
        /// </value>
        public static bool IsNaN(this Matrix matrix)
        {
            return Numeric.IsNaN(matrix.M11) || Numeric.IsNaN(matrix.M21) || Numeric.IsNaN(matrix.M31) || Numeric.IsNaN(matrix.M41)
                   || Numeric.IsNaN(matrix.M12) || Numeric.IsNaN(matrix.M22) || Numeric.IsNaN(matrix.M32) || Numeric.IsNaN(matrix.M42)
                   || Numeric.IsNaN(matrix.M13) || Numeric.IsNaN(matrix.M23) || Numeric.IsNaN(matrix.M33) || Numeric.IsNaN(matrix.M43)
                   || Numeric.IsNaN(matrix.M14) || Numeric.IsNaN(matrix.M24) || Numeric.IsNaN(matrix.M34) || Numeric.IsNaN(matrix.M44);
        }

        /// <summary>
        /// Gets a value indicating whether this matrix is symmetric.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if this matrix is symmetric; otherwise, <see langword="false"/>.
        /// </value>
        /// <remarks>
        /// The matrix elements are compared for equality - no tolerance value to handle numerical
        /// errors is used.
        /// </remarks>
        public static bool IsSymmetric(this Matrix matrix)
        {
            return matrix.M21 == matrix.M12 && matrix.M31 == matrix.M13 && matrix.M41 == matrix.M14
                   && matrix.M32 == matrix.M23 && matrix.M42 == matrix.M24
                   && matrix.M43 == matrix.M34;
        }

        /// <summary>
        /// Gets or sets the upper left 3x3 sub-matrix.
        /// </summary>
        /// <value>
        /// The 3x3 matrix that is produced by removing the last row and column of this matrix.
        /// </value>
        /// <remarks>
        /// Setting the minor matrix does not affect the elements in the fourth row or column.
        /// </remarks>
        public static Matrix Minor(this Matrix matrix)
        {
            var minorMatrix = Matrix.Identity;

            minorMatrix.M11 = matrix.M11;
            minorMatrix.M12 = matrix.M12;
            minorMatrix.M13 = matrix.M13;
            minorMatrix.M21 = matrix.M21;
            minorMatrix.M22 = matrix.M22;
            minorMatrix.M23 = matrix.M23;
            minorMatrix.M31 = matrix.M31;
            minorMatrix.M32 = matrix.M32;
            minorMatrix.M33 = matrix.M33;
            return minorMatrix;
        }

        public static void SetMinor(this Matrix matrix, Matrix minorMatrix)
        {
            matrix.M11 = minorMatrix.M11;
            matrix.M12 = minorMatrix.M12;
            matrix.M13 = minorMatrix.M13;
            matrix.M21 = minorMatrix.M21;
            matrix.M22 = minorMatrix.M22;
            matrix.M23 = minorMatrix.M23;
            matrix.M31 = minorMatrix.M31;
            matrix.M32 = minorMatrix.M32;
            matrix.M33 = minorMatrix.M33;
        }



        /// <summary>
        /// Gets the matrix trace (the sum of the diagonal elements).
        /// </summary>
        /// <value>The matrix trace.</value>
        public static float Trace(this Matrix matrix)
        {
            return matrix.M11 + matrix.M22 + matrix.M33 + matrix.M44;
        }

        /// <summary>
        /// Returns the determinant of this matrix.
        /// </summary>
        /// <value>The determinant of this matrix.</value>
        public static float Determinant(this Matrix matrix)
        {
            float m22m33_m23m32 = matrix.M33 * matrix.M44 - matrix.M43 * matrix.M34;
            float m21m33_m23m31 = matrix.M23 * matrix.M44 - matrix.M43 * matrix.M24;
            float m21m32_m22m31 = matrix.M23 * matrix.M34 - matrix.M33 * matrix.M24;
            float m20m33_m23m30 = matrix.M13 * matrix.M44 - matrix.M43 * matrix.M14;
            float m20m32_m22m30 = matrix.M13 * matrix.M34 - matrix.M33 * matrix.M14;
            float m20m31_m21m30 = matrix.M13 * matrix.M24 - matrix.M23 * matrix.M14;

            // Develop determinant after first row:
            return matrix.M11 * (matrix.M22 * m22m33_m23m32 - matrix.M32 * m21m33_m23m31 + matrix.M42 * m21m32_m22m31)
                   - matrix.M21 * (matrix.M12 * m22m33_m23m32 - matrix.M32 * m20m33_m23m30 + matrix.M42 * m20m32_m22m30)
                   + matrix.M31 * (matrix.M12 * m21m33_m23m31 - matrix.M22 * m20m33_m23m30 + matrix.M42 * m20m31_m21m30)
                   - matrix.M41 * (matrix.M12 * m21m32_m22m31 - matrix.M22 * m20m32_m22m30 + matrix.M32 * m20m31_m21m30);
        }

        public static Matrix Absolute(this Matrix matrix)
        {
            var matrixAbs = new Matrix
            {
                M11 = Math.Abs(matrix.M11),
                M21 = Math.Abs(matrix.M21),
                M31 = Math.Abs(matrix.M31),
                M41 = Math.Abs(matrix.M41),
                M12 = Math.Abs(matrix.M12),
                M22 = Math.Abs(matrix.M22),
                M32 = Math.Abs(matrix.M32),
                M42 = Math.Abs(matrix.M42),
                M13 = Math.Abs(matrix.M13),
                M23 = Math.Abs(matrix.M23),
                M33 = Math.Abs(matrix.M33),
                M43 = Math.Abs(matrix.M43),
                M14 = Math.Abs(matrix.M14),
                M24 = Math.Abs(matrix.M24),
                M34 = Math.Abs(matrix.M34),
                M44 = Math.Abs(matrix.M44)
            };

            return matrixAbs;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is orthogonal.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if this instance is an orthogonal matrix; otherwise, 
        /// <see langword="false"/>.
        /// </value>
        public static bool IsOrthogonal(this Matrix matrix)
        {
            // Orthogonal = The inverse is the same as the transposed.
            // Note: The normal Numeric.EpsilonF is too low in practice!
            return Matrix.Identity.AreNumericallyEqual(matrix * Matrix.Transpose(matrix), Numeric.EpsilonF * 10);
        }


        /// <overloads>
        /// <summary>
        /// Decomposes the matrix into the scale, translation, and rotation components.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Decomposes the matrix into the scale, translation, and rotation components.
        /// </summary>
        /// <param name="scale">The scale component of the matrix.</param>
        /// <param name="rotation">The rotation component of the matrix.</param>
        /// <param name="translation">The translation component of the matrix.</param>
        /// <returns>
        /// <see langword="true"/> if the matrix was successfully decomposed; otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method assumes that the matrix is a 3D scale/rotation/translation (SRT) matrix.
        /// <see cref="Decompose(out Vector3,out Quaternion,out Vector3)"/> returns 
        /// <see langword="false"/> when the matrix is not a valid SRT matrix. This is the case when two
        /// or more of the scale values are 0 or the last row of the matrix is something other than 
        /// (0, 0, 0, 1).
        /// </para>
        /// <para>
        /// <see cref="DecomposeFast(out Vector3,out Quaternion,out Vector3)"/> is a faster version 
        /// of this method that can be used when it is certain that the matrix is a valid SRT matrix.
        /// </para>
        /// </remarks>
        /// <example>
        /// The following example shows how to compose the matrix scale, rotation, and translation
        /// components.
        /// <code>
        /// Matrix srt = Matrix.CreateTranslation(translation)
        ///                 * Matrix.CreateRotation(rotation)
        ///                 * Matrix.CreateScale(scale);
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters")]
        public static bool Decompose(this Matrix matrix, out Vector3 scale, out Quaternion rotation, out Vector3 translation)
        {
            Matrix rotationMatrix;
            bool success = matrix.Decompose(out scale, out rotationMatrix, out translation);
            rotation = Quaternion.CreateFromRotationMatrix(rotationMatrix);
            return success;
        }

        /// <summary>
        /// Decomposes the matrix into the scale, translation, and rotation components.
        /// </summary>
        /// <param name="scale">The scale component of the matrix.</param>
        /// <param name="rotation">The rotation component of the matrix.</param>
        /// <param name="translation">The translation component of the matrix.</param>
        /// <returns>
        /// <see langword="true"/> if the matrix was successfully decomposed; otherwise, 
        /// <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method assumes that the matrix is a 3D scale/rotation/translation (SRT) matrix.
        /// <see cref="Decompose(out Vector3,out Matrix,out Vector3)"/> returns 
        /// <see langword="false"/> when the matrix is not a valid SRT matrix. This is the case when two
        /// or more of the scale values are 0 or the last row of the matrix is something other than 
        /// (0, 0, 0, 1).
        /// </para>
        /// <para>
        /// <see cref="DecomposeFast(out Vector3,out Matrix,out Vector3)"/> is a faster version of 
        /// this method that can be used when it is certain that the matrix is a valid SRT matrix.
        /// </para>
        /// </remarks>
        /// <example>
        /// The following example shows how to compose the matrix scale, rotation, and translation
        /// components.
        /// <code>
        /// Matrix sr = rotation * Matrix.CreateScale(scale);
        /// Matrix srt = new Matrix(sr, translation);
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters")]
        public static bool Decompose(this Matrix matrix, out Vector3 scale, out Matrix rotation, out Vector3 translation)
        {
            // Extract translation
            translation.X = matrix.M41;
            translation.Y = matrix.M42;
            translation.Z = matrix.M43;

            // Extract minor matrix that contains scale and rotation.
            Vector3 column0 = new Vector3(matrix.M11, matrix.M12, matrix.M13);
            Vector3 column1 = new Vector3(matrix.M21, matrix.M22, matrix.M23);
            Vector3 column2 = new Vector3(matrix.M31, matrix.M32, matrix.M33);

            // Extract scale
            scale.X = column0.Length();
            scale.Y = column1.Length();
            scale.Z = column2.Length();

            // Remove scale from minor matrix
            column0 /= scale.X;
            column1 /= scale.Y;
            column2 /= scale.Z;

            // Check whether a scale is 0.
            // If only one scale component is 0, we can still compute the rotation matrix.
            bool scaleXIsZero = Numeric.IsZero(scale.X);
            bool scaleYIsZero = Numeric.IsZero(scale.Y);
            bool scaleZIsZero = Numeric.IsZero(scale.Z);
            if (!scaleXIsZero && !scaleYIsZero && !scaleZIsZero)
            {
                rotation = new Matrix(
                    column0.X, column1.X, column2.X, 0f,
                    column0.Y, column1.Y, column2.Y, 0f,
                    column0.Z, column1.Z, column2.Z, 0f,
                    0f, 0f, 0f, 1f);

                if (!rotation.IsOrthogonal())
                {
                    rotation = Matrix.Identity;
                    return false;
                }

                if (!Numeric.AreEqual(1, rotation.Determinant()))
                {
                    // The rotation matrix contains a mirroring. We can correct this by inverting any
                    // any scale component.
                    scale.X *= -1;
                    rotation.M11 *= -1;
                    rotation.M12 *= -1;
                    rotation.M13 *= -1;
                }
            }
            else
            {
                if (scaleXIsZero)
                {
                    if (scaleYIsZero || scaleZIsZero || !Numeric.IsZero(Vector3.Dot(column1, column2)))
                    {
                        rotation = Matrix.Identity;
                        return false;
                    }

                    column0 = Vector3.Cross(column1, column2);
                }
                else if (scaleYIsZero)
                {
                    if (scaleZIsZero || !Numeric.IsZero(Vector3.Dot(column2, column0)))
                    {
                        rotation = Matrix.Identity;
                        return false;
                    }

                    column1 = Vector3.Cross(column2, column0);
                }
                else
                {
                    if (!Numeric.IsZero(Vector3.Dot(column0, column1)))
                    {
                        rotation = Matrix.Identity;
                        return false;
                    }

                    column2 = Vector3.Cross(column0, column1);
                }

                rotation = new Matrix(
                    column0.X, column1.X, column2.X, 0f,
                    column0.Y, column1.Y, column2.Y, 0f,
                    column0.Z, column1.Z, column2.Z, 0f,
                        0f, 0f, 0f, 1f);
            }

            return Numeric.IsZero(matrix.M14) && Numeric.IsZero(matrix.M24) && Numeric.IsZero(matrix.M34) && Numeric.AreEqual(matrix.M44, 1.0f);
        }

        /// <overloads>
        /// <summary>
        /// Decomposes the matrix into the scale, translation, and rotation components. (This method is
        /// faster than <see cref="Decompose(out Vector3,out Matrix,out Vector3)"/>, but the matrix 
        /// must be a valid 3D scale/rotation/translation (SRT) matrix.)
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Decomposes the matrix into the scale, translation, and rotation components. (This method is
        /// faster than <see cref="Decompose(out Vector3,out Quaternion,out Vector3)"/>, but the 
        /// matrix must be a valid 3D scale/rotation/translation (SRT) matrix.)
        /// </summary>
        /// <param name="scale">The scale component of the matrix.</param>
        /// <param name="rotation">The rotation component of the matrix.</param>
        /// <param name="translation">The translation component of the matrix.</param>
        /// <remarks>
        /// This method requires that the matrix is a 3D scale/rotation/translation (SRT) matrix. See
        /// also <see cref="Decompose(out Vector3,out Quaternion,out Vector3)"/>.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters")]
        public static void DecomposeFast(this Matrix matrix, out Vector3 scale, out Quaternion rotation, out Vector3 translation)
        {
            Matrix rotationMatrix;
            matrix.Decompose(out scale, out rotationMatrix, out translation);
            rotation = Quaternion.CreateFromRotationMatrix(rotationMatrix);
        }

        /// <summary>
        /// Decomposes the matrix into the scale, translation, and rotation components. (This method is
        /// faster than <see cref="Decompose(out Vector3,out Matrix,out Vector3)"/>, but the matrix
        /// must be a valid 3D scale/rotation/translation (SRT) matrix.)
        /// </summary>
        /// <param name="scale">The scale component of the matrix.</param>
        /// <param name="rotation">The rotation component of the matrix.</param>
        /// <param name="translation">The translation component of the matrix.</param>
        /// <remarks>
        /// This method requires that the matrix is a 3D scale/rotation/translation (SRT) matrix. See
        /// also <see cref="Decompose(out Vector3,out Matrix,out Vector3)"/>.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters")]
        public static void DecomposeFast(this Matrix matrix, out Vector3 scale, out Matrix rotation, out Vector3 translation)
        {
            // Extract translation
            translation.X = matrix.M41;
            translation.Y = matrix.M42;
            translation.Z = matrix.M43;

            // Extract minor matrix that contains scale and rotation.
            Vector3 column0 = new Vector3(matrix.M11, matrix.M12, matrix.M13);
            Vector3 column1 = new Vector3(matrix.M21, matrix.M22, matrix.M23);
            Vector3 column2 = new Vector3(matrix.M31, matrix.M32, matrix.M33);

            // Extract scale
            scale.X = column0.Length();
            scale.Y = column1.Length();
            scale.Z = column2.Length();

            // Remove scale from minor matrix
            column0 /= scale.X;
            column1 /= scale.Y;
            column2 /= scale.Z;

            // Check whether a scale is 0.
            // If only one scale component is 0, we can still compute the rotation matrix.
            bool scaleXIsZero = Numeric.IsZero(scale.X);
            bool scaleYIsZero = Numeric.IsZero(scale.Y);
            bool scaleZIsZero = Numeric.IsZero(scale.Z);
            if (!scaleXIsZero && !scaleYIsZero && !scaleZIsZero)
            {
                rotation = new Matrix(
                    column0.X, column1.X, column2.X, 0f,
                    column0.Y, column1.Y, column2.Y, 0f,
                    column0.Z, column1.Z, column2.Z, 0f,
                    0f, 0f, 0f, 1f);

                if (rotation.Determinant() < 0)
                {
                    scale.X *= -1;
                    rotation.M11 *= -1;
                    rotation.M12 *= -1;
                    rotation.M13 *= -1;
                }
            }
            else
            {
                if (scaleXIsZero)
                    column0 = Vector3.Cross(column1, column2);
                else if (scaleYIsZero)
                    column1 = Vector3.Cross(column2, column0);
                else
                    column2 = Vector3.Cross(column0, column1);

                rotation = new Matrix(
                    column0.X, column1.X, column2.X, 0f,
                    column0.Y, column1.Y, column2.Y, 0f,
                    column0.Z, column1.Z, column2.Z, 0f,
                    0f, 0f, 0f, 1f);
            }
        }

        /// <summary>
        /// Gets a column as <see cref="Vector4"/>.
        /// </summary>
        /// <param name="index">The index of the column (0, 1, 2, or 3).</param>
        /// <returns>The column vector.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="index"/> is out of range.
        /// </exception>
        public static Vector4 GetColumn(this Matrix matrix, int index)
        {
            Vector4 column;
            switch (index)
            {
                case 0:
                    column.X = matrix.M11;
                    column.Y = matrix.M12;
                    column.Z = matrix.M13;
                    column.W = matrix.M14;
                    break;
                case 1:
                    column.X = matrix.M21;
                    column.Y = matrix.M22;
                    column.Z = matrix.M23;
                    column.W = matrix.M24;
                    break;
                case 2:
                    column.X = matrix.M31;
                    column.Y = matrix.M32;
                    column.Z = matrix.M33;
                    column.W = matrix.M34;
                    break;
                case 3:
                    column.X = matrix.M41;
                    column.Y = matrix.M42;
                    column.Z = matrix.M43;
                    column.W = matrix.M44;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("index", "The column index is out of range. Allowed values are 0 to 3.");
            }

            return column;
        }

        /// <summary>
        /// Sets a column from a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="index">The index of the column (0, 1, 2, or 3).</param>
        /// <param name="columnVector">The column vector.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="index"/> is out of range.
        /// </exception>
        public static void SetColumn(this Matrix matrix, int index, Vector4 columnVector)
        {
            switch (index)
            {
                case 0:
                    matrix.M11 = columnVector.X;
                    matrix.M12 = columnVector.Y;
                    matrix.M13 = columnVector.Z;
                    matrix.M14 = columnVector.W;
                    break;
                case 1:
                    matrix.M21 = columnVector.X;
                    matrix.M22 = columnVector.Y;
                    matrix.M23 = columnVector.Z;
                    matrix.M24 = columnVector.W;
                    break;
                case 2:
                    matrix.M31 = columnVector.X;
                    matrix.M32 = columnVector.Y;
                    matrix.M33 = columnVector.Z;
                    matrix.M34 = columnVector.W;
                    break;
                case 3:
                    matrix.M41 = columnVector.X;
                    matrix.M42 = columnVector.Y;
                    matrix.M43 = columnVector.Z;
                    matrix.M44 = columnVector.W;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("index", "The column index is out of range. Allowed values are 0 to 3.");
            }
        }

        /// <summary>
        /// Gets a row as <see cref="Vector4"/>.
        /// </summary>
        /// <param name="index">The index of the row (0, 1, 2, or 3).</param>
        /// <returns>The row vector.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="index"/> is out of range.
        /// </exception>
        public static Vector4 GetRow4(this Matrix matrix, int index)
        {
            Vector4 row;
            switch (index)
            {
                case 0:
                    row.X = matrix.M11;
                    row.Y = matrix.M21;
                    row.Z = matrix.M31;
                    row.W = matrix.M41;
                    break;
                case 1:
                    row.X = matrix.M12;
                    row.Y = matrix.M22;
                    row.Z = matrix.M32;
                    row.W = matrix.M42;
                    break;
                case 2:
                    row.X = matrix.M13;
                    row.Y = matrix.M23;
                    row.Z = matrix.M33;
                    row.W = matrix.M43;
                    break;
                case 3:
                    row.X = matrix.M14;
                    row.Y = matrix.M24;
                    row.Z = matrix.M34;
                    row.W = matrix.M44;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("index", "The row index is out of range. Allowed values are 0 to 3.");
            }

            return row;
        }

        /// <summary>
        /// Gets a row as <see cref="Vector3"/>.
        /// </summary>
        /// <param name="index">The index of the row (0, 1, 2, or 3).</param>
        /// <returns>The row vector.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="index"/> is out of range.
        /// </exception>
        public static Vector3 GetRow(this Matrix matrix, int index)
        {
            Vector3 row;
            switch (index)
            {
                case 0:
                    row.X = matrix.M11;
                    row.Y = matrix.M21;
                    row.Z = matrix.M31;
                    break;
                case 1:
                    row.X = matrix.M12;
                    row.Y = matrix.M22;
                    row.Z = matrix.M32;
                    break;
                case 2:
                    row.X = matrix.M13;
                    row.Y = matrix.M23;
                    row.Z = matrix.M33;
                    break;
                case 3:
                    row.X = matrix.M14;
                    row.Y = matrix.M24;
                    row.Z = matrix.M34;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("index", "The row index is out of range. Allowed values are 0 to 3.");
            }

            return row;
        }


        /// <summary>
        /// Sets a row from a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="index">The index of the row (0, 1, 2, or 3).</param>
        /// <param name="rowVector">The row vector.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <paramref name="index"/> is out of range.
        /// </exception>
        public static void SetRow(this Matrix matrix, int index, Vector4 rowVector)
        {
            switch (index)
            {
                case 0:
                    matrix.M11 = rowVector.X;
                    matrix.M21 = rowVector.Y;
                    matrix.M31 = rowVector.Z;
                    matrix.M41 = rowVector.W;
                    break;
                case 1:
                    matrix.M12 = rowVector.X;
                    matrix.M22 = rowVector.Y;
                    matrix.M32 = rowVector.Z;
                    matrix.M42 = rowVector.W;
                    break;
                case 2:
                    matrix.M13 = rowVector.X;
                    matrix.M23 = rowVector.Y;
                    matrix.M33 = rowVector.Z;
                    matrix.M43 = rowVector.W;
                    break;
                case 3:
                    matrix.M14 = rowVector.X;
                    matrix.M24 = rowVector.Y;
                    matrix.M34 = rowVector.Z;
                    matrix.M44 = rowVector.W;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("index", "The row index is out of range. Allowed values are 0 to 3.");
            }
        }

        /// <summary>
        /// Inverts the matrix if it is invertible.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the matrix is invertible; otherwise <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method is the equivalent to <see cref="Invert"/>, except that no exceptions are thrown.
        /// The return value indicates whether the operation was successful.
        /// </para>
        /// <para>
        /// Due to numerical errors it can happen that some singular matrices are not recognized as 
        /// singular by this method. This method is optimized for fast matrix inversion and not for safe
        /// detection of singular matrices. If you need to detect if a matrix is singular, you can, for 
        /// example, compute its <see cref="Determinant"/> and see if it is near zero.
        /// </para>
        /// </remarks>
        public static bool TryInvert(this Matrix matrix)
        {
            float m22m33_m23m32 = matrix.M33 * matrix.M44 - matrix.M43 * matrix.M34;
            float m21m33_m23m31 = matrix.M23 * matrix.M44 - matrix.M43 * matrix.M24;
            float m21m32_m22m31 = matrix.M23 * matrix.M34 - matrix.M33 * matrix.M24;
            float m20m33_m23m30 = matrix.M13 * matrix.M44 - matrix.M43 * matrix.M14;
            float m20m32_m22m30 = matrix.M13 * matrix.M34 - matrix.M33 * matrix.M14;
            float m20m31_m21m30 = matrix.M13 * matrix.M24 - matrix.M23 * matrix.M14;

            float detSubMatrix00 = matrix.M22 * m22m33_m23m32 - matrix.M32 * m21m33_m23m31 + matrix.M42 * m21m32_m22m31;
            float detSubMatrix01 = matrix.M12 * m22m33_m23m32 - matrix.M32 * m20m33_m23m30 + matrix.M42 * m20m32_m22m30;
            float detSubMatrix02 = matrix.M12 * m21m33_m23m31 - matrix.M22 * m20m33_m23m30 + matrix.M42 * m20m31_m21m30;
            float detSubMatrix03 = matrix.M12 * m21m32_m22m31 - matrix.M22 * m20m32_m22m30 + matrix.M32 * m20m31_m21m30;

            // Develop determinant after first row:
            float determinant = matrix.M11 * detSubMatrix00 - matrix.M21 * detSubMatrix01 + matrix.M31 * detSubMatrix02 - matrix.M41 * detSubMatrix03;

            // We check if determinant is zero using a very small epsilon, since the determinant
            // is the result of many multiplications of potentially small numbers.
            if (Numeric.IsZero(determinant, Numeric.EpsilonFSquared * Numeric.EpsilonFSquared))
                return false;

            float detSubMatrix10 = matrix.M21 * m22m33_m23m32 - matrix.M31 * m21m33_m23m31 + matrix.M41 * m21m32_m22m31;
            float detSubMatrix11 = matrix.M11 * m22m33_m23m32 - matrix.M31 * m20m33_m23m30 + matrix.M41 * m20m32_m22m30;
            float detSubMatrix12 = matrix.M11 * m21m33_m23m31 - matrix.M21 * m20m33_m23m30 + matrix.M41 * m20m31_m21m30;
            float detSubMatrix13 = matrix.M11 * m21m32_m22m31 - matrix.M21 * m20m32_m22m30 + matrix.M31 * m20m31_m21m30;

            float m02m13_m03m12 = matrix.M31 * matrix.M42 - matrix.M41 * matrix.M32;
            float m01m13_m03m11 = matrix.M21 * matrix.M42 - matrix.M41 * matrix.M22;
            float m01m12_m02m11 = matrix.M21 * matrix.M32 - matrix.M31 * matrix.M22;
            float m00m13_m03m10 = matrix.M11 * matrix.M42 - matrix.M41 * matrix.M12;
            float m00m12_m02m10 = matrix.M11 * matrix.M32 - matrix.M31 * matrix.M12;
            float m00m11_m01m10 = matrix.M11 * matrix.M22 - matrix.M21 * matrix.M12;
            float detSubMatrix20 = matrix.M24 * m02m13_m03m12 - matrix.M34 * m01m13_m03m11 + matrix.M44 * m01m12_m02m11;
            float detSubMatrix21 = matrix.M14 * m02m13_m03m12 - matrix.M34 * m00m13_m03m10 + matrix.M44 * m00m12_m02m10;
            float detSubMatrix22 = matrix.M14 * m01m13_m03m11 - matrix.M24 * m00m13_m03m10 + matrix.M44 * m00m11_m01m10;
            float detSubMatrix23 = matrix.M14 * m01m12_m02m11 - matrix.M24 * m00m12_m02m10 + matrix.M34 * m00m11_m01m10;

            float detSubMatrix30 = matrix.M23 * m02m13_m03m12 - matrix.M33 * m01m13_m03m11 + matrix.M43 * m01m12_m02m11;
            float detSubMatrix31 = matrix.M13 * m02m13_m03m12 - matrix.M33 * m00m13_m03m10 + matrix.M43 * m00m12_m02m10;
            float detSubMatrix32 = matrix.M13 * m01m13_m03m11 - matrix.M23 * m00m13_m03m10 + matrix.M43 * m00m11_m01m10;
            float detSubMatrix33 = matrix.M13 * m01m12_m02m11 - matrix.M23 * m00m12_m02m10 + matrix.M33 * m00m11_m01m10;

            float f = 1.0f / determinant;
            matrix.M11 = detSubMatrix00 * f;
            matrix.M21 = -detSubMatrix10 * f;
            matrix.M31 = detSubMatrix20 * f;
            matrix.M41 = -detSubMatrix30 * f;

            matrix.M12 = -detSubMatrix01 * f;
            matrix.M22 = detSubMatrix11 * f;
            matrix.M32 = -detSubMatrix21 * f;
            matrix.M42 = detSubMatrix31 * f;

            matrix.M13 = detSubMatrix02 * f;
            matrix.M23 = -detSubMatrix12 * f;
            matrix.M33 = detSubMatrix22 * f;
            matrix.M43 = -detSubMatrix32 * f;

            matrix.M14 = -detSubMatrix03 * f;
            matrix.M24 = detSubMatrix13 * f;
            matrix.M34 = -detSubMatrix23 * f;
            matrix.M44 = detSubMatrix33 * f;

            return true;
        }

        /// <summary>
        /// Transforms a position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>The transformed position.</returns>
        /// <remarks>
        /// <para>
        /// By using homogeneous coordinates 4 x 4 matrices can be used to define affine transformations 
        /// or projective transformations in 3D space. When a 3D vector is given, the vector can have 
        /// multiple meanings.
        /// </para>
        /// <para>
        /// <strong>Position Vectors:</strong>
        /// A position vector identifies a point in 3D. Use <see cref="TransformPosition"/> to 
        /// transform position vectors. This method interprets the given <see cref="Vector3"/> as a 
        /// vector (x, y, z, 1) in homogeneous coordinates. The position vector is transformed by
        /// multiplication with the 4 x 4 matrix.
        /// </para>
        /// <para>
        /// <strong>Direction Vectors:</strong>
        /// A direction vector (or displacement vector) defines a direction and length in 3D. Use 
        /// <see cref="TransformDirection"/> to transform direction vectors. This method interprets the 
        /// given <see cref="Vector3"/> as a vector (x, y, z, 0) in homogeneous coordinates. The 
        /// direction vector is transformed by multiplication with the upper, left 3 x 3 corner of the
        /// transformation matrix.
        /// </para>
        /// <para>
        /// <strong>Tangent Vectors:</strong>
        /// A tangent vector (surface tangent) defines a tangential direction at a point on a surface. 
        /// They can be treated similar to direction vectors. Use <see cref="TransformDirection"/> to 
        /// transform tangent vectors and binormals vectors.
        /// </para>
        /// <para>
        /// <strong>Normal vectors:</strong>
        /// A normal vector (surface normal) is a vector that is perpendicular to the tangent plane of
        /// a given point on a surface. In differential geometry normal vectors are "tangent covectors" 
        /// or "cotangent vectors". They need to be treated differently than direction vectors or 
        /// tangent vectors. Use <see cref="TransformNormal"/> to transform normal vectors. A normal 
        /// vector is transformed by multiplication with transpose of the inverse of the upper, left 
        /// 3 x 3 corner of the transformation matrix.
        /// </para>
        /// <para>
        /// (Note: If the transformation matrix contains only rotations, translations and uniform 
        /// scalings then <see cref="TransformDirection"/> can be used to transform normal vectors,
        /// which is faster.)
        /// </para>
        /// </remarks>
        /// <see cref="TransformDirection"/>
        /// <see cref="TransformNormal"/>
        public static Vector3 TransformPosition(this Matrix matrix, Vector3 position)
        {
            float x = matrix.M11 * position.X + matrix.M21 * position.Y + matrix.M31 * position.Z + matrix.M41;
            float y = matrix.M12 * position.X + matrix.M22 * position.Y + matrix.M32 * position.Z + matrix.M42;
            float z = matrix.M13 * position.X + matrix.M23 * position.Y + matrix.M33 * position.Z + matrix.M43;
            float w = matrix.M14 * position.X + matrix.M24 * position.Y + matrix.M34 * position.Z + matrix.M44;

            // Perform homogeneous divide if necessary.
            if (!Numeric.AreEqual(w, 1f))
            {
                float oneOverW = 1 / w;
                x *= oneOverW;
                y *= oneOverW;
                z *= oneOverW;
            }

            position.X = x;
            position.Y = y;
            position.Z = z;
            return position;
        }

        /// <summary>
        /// Transforms a direction vector (or tangent vector).
        /// </summary>
        /// <param name="direction">The direction vector.</param>
        /// <returns>The transformed direction vector.</returns>
        /// <inheritdoc cref="TransformPosition"/>
        /// <see cref="TransformNormal"/>
        /// <see cref="TransformPosition"/>
        public static Vector3 TransformDirection(this Matrix matrix, Vector3 direction)
        {
            float x = matrix.M11 * direction.X + matrix.M21 * direction.Y + matrix.M31 * direction.Z;
            float y = matrix.M12 * direction.X + matrix.M22 * direction.Y + matrix.M32 * direction.Z;
            float z = matrix.M13 * direction.X + matrix.M23 * direction.Y + matrix.M33 * direction.Z;

            direction.X = x;
            direction.Y = y;
            direction.Z = z;

            return direction;
        }

        /// <summary>
        /// Transforms a normal vector.
        /// </summary>
        /// <param name="normal">The normal vector.</param>
        /// <returns>
        /// The transformed normal. (Note: The resulting vector might need to be normalized!)
        /// </returns>
        /// <inheritdoc cref="TransformPosition"/>
        /// <see cref="TransformDirection"/>
        /// <see cref="TransformPosition"/>
        public static Vector3 TransformNormal(this Matrix matrix, Vector3 normal)
        {
            // TODO: Optimization - Inline the matrix inversion.
            // When inverting a matrix using Cramer's rule we need to divide by the determinant.
            // We can leave out this division. The resulting normal vector will have a different length,
            // but the normal vector will need to be normalized anyways.

            // Multiply the transpose of the inverse with vector
            Matrix inverse = Matrix.Invert(matrix.Minor());
            float x = inverse.M11 * normal.X + inverse.M12 * normal.Y + inverse.M13 * normal.Z;
            float y = inverse.M21 * normal.X + inverse.M22 * normal.Y + inverse.M23 * normal.Z;
            float z = inverse.M31 * normal.X + inverse.M32 * normal.Y + inverse.M33 * normal.Z;

            normal.X = x;
            normal.Y = y;
            normal.Z = z;

            return normal;
        }

        /// <overloads>
        /// <summary>
        /// Determines whether two matrices are equal (regarding a given tolerance).
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Determines whether two matrices are equal (regarding the tolerance 
        /// <see cref="Numeric.EpsilonF"/>).
        /// </summary>
        /// <param name="matrix1">The first matrix.</param>
        /// <param name="matrix2">The second matrix.</param>
        /// <returns>
        /// <see langword="true"/> if the matrices are equal (within the tolerance 
        /// <see cref="Numeric.EpsilonF"/>); otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// The two matrices are compared component-wise. If the differences of the components are less
        /// than <see cref="Numeric.EpsilonF"/> the matrices are considered as being equal.
        /// </remarks>
        public static bool AreNumericallyEqual(this Matrix matrix1, Matrix matrix2)
        {
            return Numeric.AreEqual(matrix1.M11, matrix2.M11)
                   && Numeric.AreEqual(matrix1.M21, matrix2.M21)
                   && Numeric.AreEqual(matrix1.M31, matrix2.M31)
                   && Numeric.AreEqual(matrix1.M41, matrix2.M41)
                   && Numeric.AreEqual(matrix1.M12, matrix2.M12)
                   && Numeric.AreEqual(matrix1.M22, matrix2.M22)
                   && Numeric.AreEqual(matrix1.M32, matrix2.M32)
                   && Numeric.AreEqual(matrix1.M42, matrix2.M42)
                   && Numeric.AreEqual(matrix1.M13, matrix2.M13)
                   && Numeric.AreEqual(matrix1.M23, matrix2.M23)
                   && Numeric.AreEqual(matrix1.M33, matrix2.M33)
                   && Numeric.AreEqual(matrix1.M43, matrix2.M43)
                   && Numeric.AreEqual(matrix1.M14, matrix2.M14)
                   && Numeric.AreEqual(matrix1.M24, matrix2.M24)
                   && Numeric.AreEqual(matrix1.M34, matrix2.M34)
                   && Numeric.AreEqual(matrix1.M44, matrix2.M44);
        }

        /// <summary>
        /// Determines whether two matrices are equal (regarding a specific tolerance).
        /// </summary>
        /// <param name="matrix1">The first matrix.</param>
        /// <param name="matrix2">The second matrix.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>
        /// <see langword="true"/> if the matrices are equal (within the tolerance
        /// <paramref name="epsilon"/>); otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// The two matrices are compared component-wise. If the differences of the components are less
        /// than <paramref name="epsilon"/> the matrices are considered as being equal.
        /// </remarks>
        public static bool AreNumericallyEqual(this Matrix matrix1, Matrix matrix2, float epsilon)
        {
            return Numeric.AreEqual(matrix1.M11, matrix2.M11, epsilon)
                   && Numeric.AreEqual(matrix1.M21, matrix2.M21, epsilon)
                   && Numeric.AreEqual(matrix1.M31, matrix2.M31, epsilon)
                   && Numeric.AreEqual(matrix1.M41, matrix2.M41, epsilon)
                   && Numeric.AreEqual(matrix1.M12, matrix2.M12, epsilon)
                   && Numeric.AreEqual(matrix1.M22, matrix2.M22, epsilon)
                   && Numeric.AreEqual(matrix1.M32, matrix2.M32, epsilon)
                   && Numeric.AreEqual(matrix1.M42, matrix2.M42, epsilon)
                   && Numeric.AreEqual(matrix1.M13, matrix2.M13, epsilon)
                   && Numeric.AreEqual(matrix1.M23, matrix2.M23, epsilon)
                   && Numeric.AreEqual(matrix1.M33, matrix2.M33, epsilon)
                   && Numeric.AreEqual(matrix1.M43, matrix2.M43, epsilon)
                   && Numeric.AreEqual(matrix1.M14, matrix2.M14, epsilon)
                   && Numeric.AreEqual(matrix1.M24, matrix2.M24, epsilon)
                   && Numeric.AreEqual(matrix1.M34, matrix2.M34, epsilon)
                   && Numeric.AreEqual(matrix1.M44, matrix2.M44, epsilon);
        }

        /// <summary>
        /// Returns a matrix with the matrix elements clamped to the range [min, max].
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>The matrix with small elements clamped to zero.</returns>
        /// <remarks>
        /// Each matrix element is compared to zero. If it is in the interval 
        /// [-<see cref="Numeric.EpsilonF"/>, +<see cref="Numeric.EpsilonF"/>] it is set to zero, 
        /// otherwise it remains unchanged.
        /// </remarks>
        public static Matrix ClampToZero(this Matrix matrix)
        {
            matrix.M11 = Numeric.ClampToZero(matrix.M11);
            matrix.M21 = Numeric.ClampToZero(matrix.M21);
            matrix.M31 = Numeric.ClampToZero(matrix.M31);
            matrix.M41 = Numeric.ClampToZero(matrix.M41);

            matrix.M12 = Numeric.ClampToZero(matrix.M12);
            matrix.M22 = Numeric.ClampToZero(matrix.M22);
            matrix.M32 = Numeric.ClampToZero(matrix.M32);
            matrix.M42 = Numeric.ClampToZero(matrix.M42);

            matrix.M13 = Numeric.ClampToZero(matrix.M13);
            matrix.M23 = Numeric.ClampToZero(matrix.M23);
            matrix.M33 = Numeric.ClampToZero(matrix.M33);
            matrix.M43 = Numeric.ClampToZero(matrix.M43);

            matrix.M14 = Numeric.ClampToZero(matrix.M14);
            matrix.M24 = Numeric.ClampToZero(matrix.M24);
            matrix.M34 = Numeric.ClampToZero(matrix.M34);
            matrix.M44 = Numeric.ClampToZero(matrix.M44);

            return matrix;
        }

        /// <summary>
        /// Returns a matrix with the matrix elements clamped to the range [min, max].
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="epsilon">The tolerance value.</param>
        /// <returns>The matrix with small elements clamped to zero.</returns>
        /// <remarks>
        /// Each matrix element is compared to zero. If it is in the interval 
        /// [-<paramref name="epsilon"/>, +<paramref name="epsilon"/>] it is set to zero, otherwise it 
        /// remains unchanged.
        /// </remarks>
        public static Matrix ClampToZero(this Matrix matrix, float epsilon)
        {
            matrix.M11 = Numeric.ClampToZero(matrix.M11, epsilon);
            matrix.M21 = Numeric.ClampToZero(matrix.M21, epsilon);
            matrix.M31 = Numeric.ClampToZero(matrix.M31, epsilon);
            matrix.M41 = Numeric.ClampToZero(matrix.M41, epsilon);

            matrix.M12 = Numeric.ClampToZero(matrix.M12, epsilon);
            matrix.M22 = Numeric.ClampToZero(matrix.M22, epsilon);
            matrix.M32 = Numeric.ClampToZero(matrix.M32, epsilon);
            matrix.M42 = Numeric.ClampToZero(matrix.M42, epsilon);

            matrix.M13 = Numeric.ClampToZero(matrix.M13, epsilon);
            matrix.M23 = Numeric.ClampToZero(matrix.M23, epsilon);
            matrix.M33 = Numeric.ClampToZero(matrix.M33, epsilon);
            matrix.M43 = Numeric.ClampToZero(matrix.M43, epsilon);

            matrix.M14 = Numeric.ClampToZero(matrix.M14, epsilon);
            matrix.M24 = Numeric.ClampToZero(matrix.M24, epsilon);
            matrix.M34 = Numeric.ClampToZero(matrix.M34, epsilon);
            matrix.M44 = Numeric.ClampToZero(matrix.M44, epsilon);

            return matrix;
        }

        /// <overloads>
        /// <summary>
        /// Creates a scaling matrix.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Creates a scaling matrix.
        /// </summary>
        /// <param name="scale">
        /// The uniform scale factor that is applied to the x-, y-, and z-axis.
        /// </param>
        /// <returns>The created scaling matrix.</returns>
        public static Matrix CreateScale(this float scale)
        {
            Matrix result = new Matrix
            {
                M11 = scale,
                M22 = scale,
                M33 = scale,
                M44 = 1.0f
            };
            return result;
        }

        /// <summary>
        /// Creates a scaling matrix.
        /// </summary>
        /// <param name="scale">Amounts to scale by the x, y, and z-axis.</param>
        /// <returns>The created scaling matrix.</returns>
        public static Matrix CreateScale(this Vector3 scale)
        {
            Matrix result = new Matrix
            {
                M11 = scale.X,
                M22 = scale.Y,
                M33 = scale.Z,
                M44 = 1.0f
            };
            return result;
        }

        /// <overloads>
        /// <summary>
        /// Creates a rotation matrix.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Creates a rotation matrix from axis and angle.
        /// </summary>
        /// <param name="axis">The rotation axis. (Does not need to be normalized.)</param>
        /// <param name="angle">The rotation angle in radians.</param>
        /// <returns>The created rotation matrix.</returns>
        /// <exception cref="ArgumentException">
        /// The <paramref name="axis"/> vector has 0 length.
        /// </exception>
        public static Matrix CreateRotation(this Vector3 axis, float angle)
        {
            if (!axis.TryNormalize(out _))
                throw new ArgumentException("The axis vector has length 0.");

            float x = axis.X;
            float y = axis.Y;
            float z = axis.Z;
            float x2 = x * x;
            float y2 = y * y;
            float z2 = z * z;
            float xy = x * y;
            float xz = x * z;
            float yz = y * z;
            float co = (float)Math.Cos(angle);
            float si = (float)Math.Sin(angle);
            float xsi = x * si;
            float ysi = y * si;
            float zsi = z * si;
            float oneMinusCo = 1.0f - co;

            Matrix result;
            result.M11 = x2 + co * (1.0f - x2);
            result.M21 = xy * oneMinusCo - zsi;
            result.M31 = xz * oneMinusCo + ysi;
            result.M41 = 0.0f;
            result.M12 = xy * oneMinusCo + zsi;
            result.M22 = y2 + co * (1.0f - y2);
            result.M32 = yz * oneMinusCo - xsi;
            result.M42 = 0.0f;
            result.M13 = xz * oneMinusCo - ysi;
            result.M23 = yz * oneMinusCo + xsi;
            result.M33 = z2 + co * (1.0f - z2);
            result.M43 = 0.0f;
            result.M14 = 0.0f;
            result.M24 = 0.0f;
            result.M34 = 0.0f;
            result.M44 = 1.0f;
            return result;
        }

        /// <overloads>
        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// </overloads>
        /// 
        /// <summary>
        /// Creates a translation matrix from the given values.
        /// </summary>
        /// <param name="x">The translation along the x-axis.</param>
        /// <param name="y">The translation along the y-axis.</param>
        /// <param name="z">The translation along the z-axis.</param>
        /// <returns>A transformation matrix that translates vectors.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static Matrix CreateTranslation(this Matrix matrix, float x, float y, float z)
        {
            Matrix result = Matrix.Identity;
            result.M41 = x;
            result.M42 = y;
            result.M43 = z;
            result.M44 = 1;
            return result;
        }

        /// <summary>
        /// Creates a translation matrix from a vector.
        /// </summary>
        /// <param name="translation">The translation.</param>
        /// <returns>A transformation matrix that translates vectors.</returns>
        public static Matrix CreateTranslation(this Vector3 translation)
        {
            Matrix result = Matrix.Identity;
            result.M41 = translation.X;
            result.M42 = translation.Y;
            result.M43 = translation.Z;
            result.M44 = 1;
            return result;
        }

        /// <summary>
        /// Creates a matrix that specifies a rotation around the x-axis.
        /// </summary>
        /// <param name="angle">The rotation angle in radians.</param>
        /// <returns>The created rotation matrix.</returns>
        public static Matrix CreateRotationX(this float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            return new Matrix(
                1, 0, 0, 0,
                0, cos, -sin, 0,
                0, sin, cos, 0,
                0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a matrix that specifies a rotation around the y-axis.
        /// </summary>
        /// <param name="angle">The rotation angle in radians.</param>
        /// <returns>The created rotation matrix.</returns>
        public static Matrix CreateRotationY(this float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            return new Matrix(
                cos, 0, sin, 0,
                0, 1, 0, 0,
                -sin, 0, cos, 0,
                0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a matrix that specifies a rotation around the z-axis.
        /// </summary>
        /// <param name="angle">The rotation angle in radians.</param>
        /// <returns>The created rotation matrix.</returns>
        public static Matrix CreateRotationZ(this float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            return new Matrix(
                cos, -sin, 0, 0,
                sin, cos, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a right-handed, orthographic projection matrix. (Only available in the XNA 
        /// compatible build.)
        /// </summary>
        /// <param name="width">The width of the view volume.</param>
        /// <param name="height">The height of the view volume.</param>
        /// <param name="zNear">
        /// The minimum z-value of the view volume. (Distance of the near view plane.)
        /// </param>
        /// <param name="zFar">
        /// The maximum z-value of the view volume. (Distance of the far view plane.)
        /// </param>
        /// <returns>The right-handed orthographic projection matrix.</returns>
        /// <remarks>
        /// <para>
        /// This method is available only in the XNA-compatible build of the 
        /// DigitalRune.Mathematics.dll.
        /// </para>
        /// <para>
        /// In contrast to all preceding coordinate spaces (model space, world space, view space) the 
        /// projection space is left-handed! This is necessary because DirectX uses a left-handed 
        /// clip space.
        /// </para>
        /// <para>
        /// In the projection space the x and y-coordinates range from −1 to 1, and the z-coordinates
        /// range from 0 (near) to 1 (far).
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> is 0.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="zNear"/> is greater than or equal to <paramref name="zFar"/>.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static Matrix CreateOrthographic(float width, float height, float zNear, float zFar)
        {
            // See DirectX, D3DXMatrixOrthoRH().

            if (width == 0)
                throw new ArgumentOutOfRangeException("width", "The width must greater than 0.");
            if (height == 0)
                throw new ArgumentOutOfRangeException("height", "The height must greater than 0.");
            if (zNear >= zFar)
                throw new ArgumentException("The distance to the near view plane must be less than the distance to the far view plane (zNear < zFar).");

            return new Matrix(
                2 / width, 0, 0, 0,
                0, 2 / height, 0, 0,
                0, 0, 1 / (zNear - zFar), zNear / (zNear - zFar),
                0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a customized (off-center), right-handed, orthographic projection matrix. (Only 
        /// available in the XNA-compatible build.)
        /// </summary>
        /// <param name="left">The minimum x-value of the view volume.</param>
        /// <param name="right">The maximum x-value of the view volume.</param>
        /// <param name="bottom">The minimum y-value of the view volume.</param>
        /// <param name="top">The maximum y-value of the view volume.</param>
        /// <param name="zNear">
        /// The minimum z-value of the view volume. (Distance of the near view plane.)
        /// </param>
        /// <param name="zFar">
        /// The maximum z-value of the view volume. (Distance of the far view plane.)
        /// </param>
        /// <returns>The customized (off-center), right-handed orthographic projection matrix.</returns>
        /// <remarks>
        /// <para>
        /// This method is available only in the XNA-compatible build of the 
        /// DigitalRune.Mathematics.dll.
        /// </para>
        /// <para>
        /// In contrast to all preceding coordinate spaces (model space, world space, view space) the 
        /// projection space is left-handed! This is necessary because DirectX uses a left-handed 
        /// clip space.
        /// </para>
        /// <para>
        /// In the projection space the x and y-coordinates range from −1 to 1, and the z-coordinates
        /// range from 0 (near) to 1 (far).
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// <paramref name="left"/> is equal to <paramref name="right"/>, 
        /// <paramref name="bottom"/> is equal to <paramref name="top"/>, or
        /// <paramref name="zNear"/> is greater than or equal to <paramref name="zFar"/>.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static Matrix CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNear, float zFar)
        {
            // See DirectX, D3DXMatrixOrthoOffCenterRH().

            if (left == right)
                throw new ArgumentException("The minimum x-value (left) must not be equal to the maximum x-value (right).");
            if (bottom == top)
                throw new ArgumentException("The minimum y-value (bottom) must not be equal to the maximum y-value (top).");
            if (zNear >= zFar)
                throw new ArgumentException("The distance to the near view plane must be less than the distance to the far view plane (zNear < zFar).");

            return new Matrix(
                2 / (right - left), 0, 0, (left + right) / (left - right),
                0, 2 / (top - bottom), 0, (top + bottom) / (bottom - top),
                0, 0, 1 / (zNear - zFar), zNear / (zNear - zFar),
                0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a right-handed, perspective projection matrix. (Only available in the XNA-compatible 
        /// build.)
        /// </summary>
        /// <param name="width">The width of the view volume at the near view-plane.</param>
        /// <param name="height">The height of the view volume at the near view-plane.</param>
        /// <param name="zNear">
        /// The minimum z-value of the view volume. (Distance of the near view plane.)
        /// </param>
        /// <param name="zFar">
        /// The maximum z-value of the view volume. (Distance of the far view plane.)
        /// </param>
        /// <returns>The right-handed, perspective projection matrix.</returns>
        /// <remarks>
        /// <para>
        /// This method is available only in the XNA-compatible build of the 
        /// DigitalRune.Mathematics.dll.
        /// </para>
        /// <para>
        /// In contrast to all preceding coordinate spaces (model space, world space, view space) the 
        /// projection space is left-handed! This is necessary because DirectX uses a left-handed 
        /// clip space.
        /// </para>
        /// <para>
        /// In the projection space the x and y-coordinates range from −1 to 1, and the z-coordinates
        /// range from 0 (near) to 1 (far).
        /// </para>
        /// <para>
        /// <strong>Infinite Projections:</strong><br/>
        /// <paramref name="zFar"/> can be set to <see cref="float.PositiveInfinity"/> to create an
        /// <i>infinite projection</i> where the far clip plane is at infinity.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// A parameter is negative or 0.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="zNear"/> is greater than or equal to <paramref name="zFar"/>.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static Matrix CreatePerspective(float width, float height, float zNear, float zFar)
        {
            // See DirectX, D3DXMatrixPerspectiveRH()
            //
            // For optimal precision zFar and zNear should be chosen such that
            //
            //             zFar                     zNear * zFar
            //   M44 = ------------    and    M34 = ------------
            //         zNear - zFar                 zNear - zFar
            //
            // have an exact floating-point representation.
            //
            // The infinite projection matrix Pinf is the same as the projection matrix,
            // except that the far clip plane is at ∞.
            //
            //   Pinf =   limit P
            //          zFar --> ∞
            //
            // The infinite projection compresses z values only slightly more than a finite
            // projection. However, [2] shows that the floating-point precision of an infinite
            // projection is actually better(!) than the precision of a finite projection matrix.
            //
            // References:
            // [1] Cass Everitt, Mark J. Kilgard: Practical and Robust Stenciled Shadow Volumes for Hardware-Accelerated Rendering
            // [2] Paul Upchurch and Mathieu Desbrun: Tightening the Precision of Perspective Rendering

            if (width == 0)
                throw new ArgumentOutOfRangeException("width", "The width must not be 0.");
            if (height == 0)
                throw new ArgumentOutOfRangeException("height", "The height must not be 0.");
            if (zNear <= 0)
                throw new ArgumentOutOfRangeException("zNear", "The distance to the near view plane must not be negative or 0.");
            if (zFar <= 0)
                throw new ArgumentOutOfRangeException("zFar", "The distance to the far view plane must not be negative or 0.");
            if (zNear >= zFar)
                throw new ArgumentException("The distance to the near view plane must be less than the distance to the far view plane (zNear < zFar).");

            if (double.IsPositiveInfinity(zFar))
            {
                // Infinite projection.
                return new Matrix(
                    2 * zNear / width, 0, 0, 0,
                    0, 2 * zNear / height, 0, 0,
                    0, 0, -1, -zNear,
                    0, 0, -1, 0);
            }

            return new Matrix(
                2 * zNear / width, 0, 0, 0,
                0, 2 * zNear / height, 0, 0,
                0, 0, zFar / (zNear - zFar), zNear * zFar / (zNear - zFar),
                0, 0, -1, 0);
        }

        /// <summary>
        /// Creates a right-handed, perspective projection matrix based on a field of view. (Only 
        /// available in the XNA-compatible build.)
        /// </summary>
        /// <param name="fieldOfViewY">The vertical field of view.</param>
        /// <param name="aspectRatio">The aspect ratio (width / height).</param>
        /// <param name="zNear">
        /// The minimum z-value of the view volume. (Distance of the near view plane.)
        /// </param>
        /// <param name="zFar">
        /// The maximum z-value of the view volume. (Distance of the far view plane.)
        /// </param>
        /// <returns>
        /// The right-handed, perspective projection matrix.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method is available only in the XNA-compatible build of the 
        /// DigitalRune.Mathematics.dll.
        /// </para>
        /// <para>
        /// In contrast to all preceding coordinate spaces (model space, world space, view space) the
        /// projection space is left-handed! This is necessary because DirectX uses a left-handed
        /// clip space.
        /// </para>
        /// <para>
        /// In the projection space the x and y-coordinates range from −1 to 1, and the z-coordinates
        /// range from 0 (near) to 1 (far).
        /// </para>
        /// <para>
        /// <strong>Infinite Projections:</strong><br/>
        /// <paramref name="zFar"/> can be set to <see cref="float.PositiveInfinity"/> to create an
        /// <i>infinite projection</i> where the far clip plane is at infinity.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="fieldOfViewY"/> is not between 0 and π radians (0° and 180°),
        /// <paramref name="aspectRatio"/> is negative or 0, <paramref name="zNear"/> is negative or 0,
        /// or <paramref name="zFar"/> is negative or 0.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="zNear"/> is greater than or equal to <paramref name="zFar"/>.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static Matrix CreatePerspectiveFieldOfView(float fieldOfViewY, float aspectRatio, float zNear, float zFar)
        {
            // See DirectX, D3DXMatrixPerspectiveFovRH().

            if (fieldOfViewY <= 0 || fieldOfViewY >= Math.PI)
                throw new ArgumentOutOfRangeException("fieldOfViewY", "The field of view must be between 0 radians and π radians.");
            if (aspectRatio <= 0)
                throw new ArgumentOutOfRangeException("aspectRatio", "The aspect ratio must not be negative or 0.");
            if (zNear <= 0)
                throw new ArgumentOutOfRangeException("zNear", "The distance to the near view plane must not be negative or 0.");
            if (zFar <= 0)
                throw new ArgumentOutOfRangeException("zFar", "The distance to the far view plane must not be negative or 0.");
            if (zNear >= zFar)
                throw new ArgumentException("The distance to the near view plane must be less than the distance to the far view plane (zNear < zFar).");

            float yScale = 1.0f / (float)Math.Tan(0.5f * fieldOfViewY);
            float xScale = yScale / aspectRatio;

            if (double.IsPositiveInfinity(zFar))
            {
                // Infinite projection.
                return new Matrix(
                    xScale, 0, 0, 0,
                    0, yScale, 0, 0,
                    0, 0, -1, -zNear,
                    0, 0, -1, 0);
            }

            return new Matrix(
                xScale, 0, 0, 0,
                0, yScale, 0, 0,
                0, 0, zFar / (zNear - zFar), zNear * zFar / (zNear - zFar),
                0, 0, -1, 0);
        }

        /// <summary>
        /// Creates a customized, right-handed, perspective projection matrix. (Only available in the 
        /// XNA-compatible build.)
        /// </summary>
        /// <param name="left">The minimum x-value of the view volume at the near view plane.</param>
        /// <param name="right">The maximum x-value of the view volume at the near view plane.</param>
        /// <param name="bottom">The minimum y-value of the view volume at the near view plane.</param>
        /// <param name="top">The maximum y-value of the view volume at the near view plane.</param>
        /// <param name="zNear">
        /// The minimum z-value of the view volume. (Distance of the near view plane.)
        /// </param>
        /// <param name="zFar">
        /// The maximum z-value of the view volume. (Distance of the far view plane.)
        /// </param>
        /// <returns>
        /// The customized, right-handed, perspective projection matrix.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method is available only in the XNA-compatible build of the 
        /// DigitalRune.Mathematics.dll.
        /// </para>
        /// <para>
        /// In contrast to all preceding coordinate spaces (model space, world space, view space) the
        /// projection space is left-handed! This is necessary because DirectX uses a left-handed
        /// clip space.
        /// </para>
        /// <para>
        /// In the projection space the x and y-coordinates range from −1 to 1, and the z-coordinates
        /// range from 0 (near) to 1 (far).
        /// </para>
        /// <para>
        /// <strong>Infinite Projections:</strong><br/>
        /// <paramref name="zFar"/> can be set to <see cref="float.PositiveInfinity"/> to create an
        /// <i>infinite projection</i> where the far clip plane is at infinity.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="zNear"/> or <paramref name="zFar"/> is negative or 0.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="left"/> is equal to <paramref name="right"/>,
        /// <paramref name="bottom"/> is equal to <paramref name="top"/>, or 
        /// <paramref name="zNear"/> is greater than or equal to <paramref name="zFar"/>.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static Matrix CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float zNear, float zFar)
        {
            // See DirectX, D3DXMatrixPerspectiveOffCenterRH().

            if (left == right)
                throw new ArgumentException("The minimum x-value (left) must not be equal to the maximum x-value (right).");
            if (bottom == top)
                throw new ArgumentException("The minimum y-value (bottom) must not be equal to the maximum y-value (top).");
            if (zNear <= 0)
                throw new ArgumentOutOfRangeException("zNear", "The distance to the near view plane must not be negative or 0.");
            if (zFar <= 0)
                throw new ArgumentOutOfRangeException("zFar", "The distance to the far view plane must not be negative or 0.");
            if (zNear >= zFar)
                throw new ArgumentException("The distance to the near view plane must be less than the distance to the far view plane (zNear < zFar).");

            if (double.IsPositiveInfinity(zFar))
            {
                // Infinite projection.
                return new Matrix(
                    2 * zNear / (right - left), 0, (left + right) / (right - left), 0,
                    0, 2 * zNear / (top - bottom), (top + bottom) / (top - bottom), 0,
                    0, 0, -1, -zNear,
                    0, 0, -1, 0);
            }

            return new Matrix(
                2 * zNear / (right - left), 0, (left + right) / (right - left), 0,
                0, 2 * zNear / (top - bottom), (top + bottom) / (top - bottom), 0,
                0, 0, zFar / (zNear - zFar), zNear * zFar / (zNear - zFar),
                0, 0, -1, 0);
        }

    }
}

