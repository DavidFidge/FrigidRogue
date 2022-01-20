using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.TestInfrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Tests.Services
{
    [TestClass]
    public class RayExtensionsTests : BaseTest
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }

        [TestMethod]
        public void ClipToZ_Should_Clip_Ray_Position_To_Z_Max_And_Ray_Direction_To_Z_Min()
        {
            // Arrange
            var position = new Vector3(20, 0, 10);
            var direction = new Vector3(10, 0, -20);
            var ray = new Ray(position, direction);

            // Act
            var result = ray.ClipToZ(5, -6);

            // Assert
            Assert.AreEqual(new Vector3(22.5f, 0, 5), result.Position);
            Assert.AreEqual(new Vector3(5.5f, 0, -11f), result.Direction);
        }

        [TestMethod]
        [DataRow(0f)]
        [DataRow(0.00001f)]
        [DataRow(-0.00001f)]
        public void ClipToZ_Should_Return_Original_Ray_When_Z_Direction_Close_To_Zero(float z)
        {
            // Arrange
            var position = new Vector3(0, 0, 10);
            var direction = new Vector3(0, 0, 0);
            var ray = new Ray(position, direction);

            // Act
            var result = ray.ClipToZ(5, -6);

            // Assert
            Assert.AreEqual(position, result.Position);
            Assert.AreEqual(direction, result.Direction);
        }
    }
}