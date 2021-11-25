using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Tests.Services
{
    public static class MatrixExtensions
    {
        public static void AreEquivalent(this Assert assert, Matrix expected, Matrix actual)
        {
            Assert.That.AreEquivalent(expected.M11, actual.M11);
            Assert.That.AreEquivalent(expected.M12, actual.M12);
            Assert.That.AreEquivalent(expected.M13, actual.M13);
            Assert.That.AreEquivalent(expected.M14, actual.M14);

            Assert.That.AreEquivalent(expected.M21, actual.M21);
            Assert.That.AreEquivalent(expected.M22, actual.M22);
            Assert.That.AreEquivalent(expected.M23, actual.M23);
            Assert.That.AreEquivalent(expected.M24, actual.M24);

            Assert.That.AreEquivalent(expected.M31, actual.M31);
            Assert.That.AreEquivalent(expected.M32, actual.M32);
            Assert.That.AreEquivalent(expected.M33, actual.M33);
            Assert.That.AreEquivalent(expected.M34, actual.M34);

            Assert.That.AreEquivalent(expected.M41, actual.M41);
            Assert.That.AreEquivalent(expected.M42, actual.M42);
            Assert.That.AreEquivalent(expected.M43, actual.M43);
            Assert.That.AreEquivalent(expected.M44, actual.M44);
        }

        public static void AreNotEquivalent(this Assert assert, Matrix expected, Matrix actual)
        {
            try
            {
                Assert.That.AreEquivalent(expected.M11, actual.M11);
                Assert.That.AreEquivalent(expected.M12, actual.M12);
                Assert.That.AreEquivalent(expected.M13, actual.M13);
                Assert.That.AreEquivalent(expected.M14, actual.M14);

                Assert.That.AreEquivalent(expected.M21, actual.M21);
                Assert.That.AreEquivalent(expected.M22, actual.M22);
                Assert.That.AreEquivalent(expected.M23, actual.M23);
                Assert.That.AreEquivalent(expected.M24, actual.M24);

                Assert.That.AreEquivalent(expected.M31, actual.M31);
                Assert.That.AreEquivalent(expected.M32, actual.M32);
                Assert.That.AreEquivalent(expected.M33, actual.M33);
                Assert.That.AreEquivalent(expected.M34, actual.M34);

                Assert.That.AreEquivalent(expected.M41, actual.M41);
                Assert.That.AreEquivalent(expected.M42, actual.M42);
                Assert.That.AreEquivalent(expected.M43, actual.M43);
                Assert.That.AreEquivalent(expected.M44, actual.M44);
            }
            catch (AssertFailedException)
            {
                // if any 'positive' assertion fails then it means that
                // the matrices are not equivalent
                return;
            }

            Assert.Fail("Matrices are equivalent", expected, actual);
        }
    }
}