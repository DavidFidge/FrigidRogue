using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Tests.Services
{
    public static class Vector3Extensions
    {
        public static void AreEquivalent(this Assert assert, Vector3 expected, Vector3 actual)
        {
            Assert.That.AreEquivalent(expected.X, actual.X);
            Assert.That.AreEquivalent(expected.Y, actual.Y);
            Assert.That.AreEquivalent(expected.Z, actual.Z);
        }
    }
}