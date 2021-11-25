using System.Collections.Generic;
using System.Linq;

using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.TestInfrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using NGenerics.Extensions;

namespace FrigidRogue.MonoGame.Core.Tests.Services
{
    [TestClass]
    public class PointExtensionsTests : BaseTest
    {

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

        }

        [TestMethod]
        public void SurroundingPoints_Should_Return_All_Surrounding_Points()
        {
            // Arrange
            var point = new Point();

            // Act
            var result = point.SurroundingPoints();

            // Assert
            var expectedPoints = new List<Point>
            {
                new Point(-1, -1),
                new Point(-1, 0),
                new Point(-1, 1),
                new Point(0, -1),
                new Point(0, 1),
                new Point(1, -1),
                new Point(1, 0),
                new Point(1, 1)
            };

            CollectionAssert.AreEquivalent(expectedPoints, result);
        }

        [TestMethod]
        public void SurroundingPoints_Should_Return_All_Surrounding_Points_Limited_By_X_Min()
        {
            // Arrange
            var point = new Point();

            // Act
            var result = point.SurroundingPoints(0);

            // Assert
            var expectedPoints = new List<Point>
            {
                new Point(0, -1),
                new Point(0, 1),
                new Point(1, -1),
                new Point(1, 0),
                new Point(1, 1),
            };

            CollectionAssert.AreEquivalent(expectedPoints, result);
        }


        [TestMethod]
        public void SurroundingPoints_Should_Return_All_Surrounding_Points_Limited_By_X_Max()
        {
            // Arrange
            var point = new Point();

            // Act
            var result = point.SurroundingPoints(xMax: 0);

            // Assert
            var expectedPoints = new List<Point>
            {
                new Point(-1, -1),
                new Point(-1, 0),
                new Point(-1, 1),
                new Point(0, -1),
                new Point(0, 1)
            };

            CollectionAssert.AreEquivalent(expectedPoints, result);
        }

        [TestMethod]
        public void SurroundingPoints_Should_Return_All_Surrounding_Points_Limited_By_Y_Min()
        {
            // Arrange
            var point = new Point();

            // Act
            var result = point.SurroundingPoints(yMin: 0);

            // Assert
            var expectedPoints = new List<Point>
            {
                new Point(-1, 0),
                new Point(-1, 1),
                new Point(0, 1),
                new Point(1, 0),
                new Point(1, 1)
            };

            CollectionAssert.AreEquivalent(expectedPoints, result);
        }

        [TestMethod]
        public void SurroundingPoints_Should_Return_All_Surrounding_Points_Limited_By_Y_Max()
        {
            // Arrange
            var point = new Point();

            // Act
            var result = point.SurroundingPoints(yMax: 0);

            // Assert
            var expectedPoints = new List<Point>
            {
                new Point(-1, -1),
                new Point(-1, 0),
                new Point(0, -1),
                new Point(1, -1),
                new Point(1, 0)
            };

            CollectionAssert.AreEquivalent(expectedPoints, result);
        }

        [TestMethod]
        public void SurroundingPoints_Should_Return_No_Points_If_Limit_Excludes_All()
        {
            // Arrange
            var point = new Point();

            // Act
            var result = point.SurroundingPoints(2);

            // Assert
            Assert.IsTrue(result.IsEmpty());
        }
    }
}