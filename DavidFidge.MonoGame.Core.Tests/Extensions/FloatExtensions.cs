using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DavidFidge.MonoGame.Core.Tests.Services
{
    public static class FloatExtensions
    {
        public static void AreEquivalent(this Assert assert, float expected, float actual)
        {
            if (float.IsNaN(expected) || float.IsNaN(actual))
            {
                Assert.IsTrue(float.IsNaN(expected) && float.IsNaN(actual));
                return;
            }

            if (float.IsNegativeInfinity(expected) || float.IsNegativeInfinity(actual))
            {
                Assert.IsTrue(float.IsNegativeInfinity(expected) && float.IsNegativeInfinity(actual));

                return;
            }

            if (float.IsPositiveInfinity(expected) || float.IsPositiveInfinity(actual))
            {
                Assert.IsTrue(float.IsPositiveInfinity(expected) && float.IsPositiveInfinity(actual));
                return;
            }

            Assert.IsTrue(Math.Abs(expected - actual) < 0.00001f);
        }

        public static void AreNotEquivalent(this Assert assert, float expected, float actual)
        {
            if (float.IsNaN(expected) || float.IsNaN(actual))
            {
                Assert.IsFalse(float.IsNaN(expected) && float.IsNaN(actual));
                return;
            }

            if (float.IsNegativeInfinity(expected) || float.IsNegativeInfinity(actual))
            {
                Assert.IsFalse(float.IsNegativeInfinity(expected) && float.IsNegativeInfinity(actual));

                return;
            }

            if (float.IsPositiveInfinity(expected) || float.IsPositiveInfinity(actual))
            {
                Assert.IsFalse(float.IsPositiveInfinity(expected) && float.IsPositiveInfinity(actual));
                return;
            }

            Assert.IsFalse(Math.Abs(expected - actual) < 0.00001f);
        }
    }
}