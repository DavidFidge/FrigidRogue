using System;

using DavidFidge.MonoGame.Core.Extensions;
using DavidFidge.TestInfrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace DavidFidge.MonoGame.Core.Tests.Services
{
    [TestClass]
    public class Vector3ExtensionsTests : BaseTest
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }

        [TestMethod]
        public void Should_Truncate_To_Given_Length()
        {
            // Arrange
            var vector = new Vector3(1, 1, 1);

            // Act
            var result = vector.Truncate(1);

            // Assert
            var expectedResult = new Vector3((float)Math.Sqrt(1f / 3));
            Assert.That.AreEquivalent(result, expectedResult);
        }

        [TestMethod]
        public void Should_Not_Truncate_If_Less_Than_Given_Length()
        {
            // Arrange
            var vector = new Vector3(1, 1, 1);

            // Act
            var result = vector.Truncate(2.1f);

            // Assert
            Assert.That.AreEquivalent(result, vector);
        }
    }
}