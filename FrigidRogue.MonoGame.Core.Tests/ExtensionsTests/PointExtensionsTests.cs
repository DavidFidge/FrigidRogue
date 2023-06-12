using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.TestInfrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;

namespace FrigidRogue.MonoGame.Core.Tests.Extensions
{
    [TestClass]
    public class PointExtensionsTests : BaseTest
    {
        [TestMethod]
        public void Should_Split_Points_50Percent()
        {
            // Arrange
            var points = new List<Point>();

            for (var x = 0; x < 3; x++)
            for (var y = 0; y < 3; y++)
                points.Add(new Point(x, y));

            var targetPoint = new List<Point> { new Point(0, 0) };

            // Act
            var result = points.SplitIntoPointsBySumMagnitudeAgainstTargetPoints(targetPoint);

            // Assert
            Assert.AreEqual(4, result.Item1.Count);
            Assert.IsTrue(result.Item1.Contains(new Point(0, 0)));
            Assert.IsTrue(result.Item1.Contains(new Point(0, 1)));
            Assert.IsTrue(result.Item1.Contains(new Point(1, 0)));
            Assert.IsTrue(result.Item1.Contains(new Point(1, 1)));

            Assert.AreEqual(5, result.Item2.Count);
            Assert.IsTrue(result.Item2.Contains(new Point(2, 0)));
            Assert.IsTrue(result.Item2.Contains(new Point(2, 1)));
            Assert.IsTrue(result.Item2.Contains(new Point(2, 2)));
            Assert.IsTrue(result.Item2.Contains(new Point(0, 2)));
            Assert.IsTrue(result.Item2.Contains(new Point(1, 2)));
        }

        [TestMethod]
        public void Should_Split_Points_ZeroPercent()
        {
            // Arrange
            var points = new List<Point>();

            for (var x = 0; x < 3; x++)
            for (var y = 0; y < 3; y++)
                points.Add(new Point(x, y));

            var targetPoint = new List<Point> { new Point(0, 0) };

            // Act
            var result = points.SplitIntoPointsBySumMagnitudeAgainstTargetPoints(targetPoint, 0);

            // Assert
            Assert.AreEqual(0, result.Item1.Count);

            Assert.AreEqual(9, result.Item2.Count);
            CollectionAssert.AreEquivalent(points, result.Item2.ToList());
        }

        [TestMethod]
        public void Should_Split_Points_100Percent()
        {
            // Arrange
            var points = new List<Point>();

            for (var x = 0; x < 3; x++)
            for (var y = 0; y < 3; y++)
                points.Add(new Point(x, y));

            var targetPoint = new List<Point> { new Point(0, 0) };

            // Act
            var result = points.SplitIntoPointsBySumMagnitudeAgainstTargetPoints(targetPoint, 1);

            // Assert
            Assert.AreEqual(9, result.Item1.Count);
            CollectionAssert.AreEquivalent(points, result.Item1.ToList());

            Assert.AreEqual(0, result.Item2.Count);
        }

        [TestMethod]
        public void Neighbours_Should_Return_All_Surrounding_Points()
        {
            // Arrange
            var point = new Point();

            // Act
            var result = point.Neighbours();

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
        public void NeighboursOutwardsFrom_Should_Return_All_Surrounding_Points()
        {
            // Arrange
            var point = new Point();

            // Act
            var result = point.NeighboursOutwardsFrom(2, -5, 5, -5, 5);

            // Assert
            var expectedPoints = new List<Point>
            {
                new Point(-2, -2),
                new Point(-2, -1),
                new Point(-2, -0),
                new Point(-2, 1),
                new Point(-2, 2),
                new Point(-1, -2),
                new Point(-1, -1),
                new Point(-1, -0),
                new Point(-1, 1),
                new Point(-1, 2),
                new Point(0, -2),
                new Point(0, -1),
                new Point(0, 1),
                new Point(0, 2),
                new Point(1, -2),
                new Point(1, -1),
                new Point(1, -0),
                new Point(1, 1),
                new Point(1, 2),
                new Point(2, -2),
                new Point(2, -1),
                new Point(2, -0),
                new Point(2, 1),
                new Point(2, 2)
            };

            CollectionAssert.AreEquivalent(expectedPoints, result);
        }

        [TestMethod]
        public void NeighboursOutwardsFrom_Should_Return_All_Surrounding_Points_Limited_By_MinMax()
        {
            // Arrange
            var point = new Point();

            // Act
            var result = point.NeighboursOutwardsFrom(10, -1, 1, -1, 1);

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
        public void Neighbours_Should_Return_Surrounding_Points_For_Cardinal_Direction()
        {
            // Arrange
            var point = new Point();

            // Act
            var result = point.Neighbours(adjacencyRule: AdjacencyRule.Cardinals);

            // Assert
            var expectedPoints = new List<Point>
            {
                new Point(-1, 0),
                new Point(0, -1),
                new Point(0, 1),
                new Point(1, 0),
            };

            CollectionAssert.AreEquivalent(expectedPoints, result);
        }

        [TestMethod]
        public void Neighbours_Should_Return_Surrounding_Points_For_Diagonal_Direction()
        {
            // Arrange
            var point = new Point();

            // Act
            var result = point.Neighbours(adjacencyRule: AdjacencyRule.Diagonals);

            // Assert
            var expectedPoints = new List<Point>
            {
                new Point(-1, -1),
                new Point(-1, 1),
                new Point(1, -1),
                new Point(1, 1)
            };

            CollectionAssert.AreEquivalent(expectedPoints, result);
        }
        
        [TestMethod]
        public void Neighbours_Should_Return_All_Surrounding_Points_Limited_By_X_Min()
        {
            // Arrange
            var point = new Point();

            // Act
            var result = point.Neighbours(xMin: 0);

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
        public void Neighbours_Should_Return_All_Surrounding_Points_Limited_By_X_Max()
        {
            // Arrange
            var point = new Point();

            // Act
            var result = point.Neighbours(xMax: 0);

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
        public void Neighbours_Should_Return_All_Surrounding_Points_Limited_By_Y_Min()
        {
            // Arrange
            var point = new Point();

            // Act
            var result = point.Neighbours(yMin: 0);

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
        public void Neighbours_Should_Return_All_Surrounding_Points_Limited_By_Y_Max()
        {
            // Arrange
            var point = new Point();

            // Act
            var result = point.Neighbours(yMax: 0);

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
        public void Neighbours_Should_Return_No_Points_If_Limit_Excludes_All()
        {
            // Arrange
            var point = new Point();

            // Act
            var result = point.Neighbours(xMin: 2);

            // Assert
            Assert.IsTrue(result.IsEmpty());
        }
        
        [TestMethod]
        public void Neighbours_With_SettableGridView_Should_Return_Surrounding_Points_Within_Width_And_Height_Min()
        {
            // Arrange
            var point = new Point();
            var settableGridView = new ArrayView<bool>(2, 2);

            // Act
            var result = point.Neighbours(settableGridView);

            // Assert
            var expectedPoints = new List<Point>
            {
                new Point(0, 1),
                new Point(1, 0),
                new Point(1, 1)
            };

            CollectionAssert.AreEquivalent(expectedPoints, result);
        }

        [TestMethod]
        public void Neighbours_With_SettableGridView_Should_Return_Surrounding_Points_Within_Width_And_Height_Max()
        {
            // Arrange
            var point = new Point(2, 2);
            var settableGridView = new ArrayView<bool>(3, 3);

            // Act
            var result = point.Neighbours(settableGridView);

            // Assert
            var expectedPoints = new List<Point>
            {
                new Point(1, 1),
                new Point(1, 2),
                new Point(2, 1)
            };

            CollectionAssert.AreEquivalent(expectedPoints, result);
        }

        [TestMethod]
        [DataRow(2, 2, false)]
        [DataRow(0, 0, false)]
        [DataRow(0, 1, false)]
        [DataRow(0, 2, false)]
        [DataRow(0, 3, false)]
        [DataRow(0, 4, false)]
        [DataRow(4, 0, false)]
        [DataRow(4, 1, false)]
        [DataRow(4, 2, false)]
        [DataRow(4, 3, false)]
        [DataRow(4, 4, false)]
        [DataRow(2, 0, false)]
        [DataRow(2, 4, false)]
        [DataRow(1, 1, true)]
        [DataRow(1, 2, true)]
        [DataRow(1, 3, true)]
        [DataRow(2, 1, true)]
        [DataRow(2, 3, true)]
        [DataRow(3, 1, true)]
        [DataRow(3, 2, true)]
        [DataRow(3, 3, true)]
        public void IsNextTo_For_8Way(int x, int y, bool expectedResult)
        {
            // Arrange
            var point1 = new Point(2, 2);
            var point2 = new Point(x, y);

            // Act
            var result = point1.IsNextTo(point2, AdjacencyRule.EightWay);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [DataRow(2, 2, false)]
        [DataRow(0, 0, false)]
        [DataRow(0, 1, false)]
        [DataRow(0, 2, false)]
        [DataRow(0, 3, false)]
        [DataRow(0, 4, false)]
        [DataRow(4, 0, false)]
        [DataRow(4, 1, false)]
        [DataRow(4, 2, false)]
        [DataRow(4, 3, false)]
        [DataRow(4, 4, false)]
        [DataRow(2, 0, false)]
        [DataRow(2, 4, false)]
        [DataRow(1, 1, false)]
        [DataRow(1, 2, true)]
        [DataRow(1, 3, false)]
        [DataRow(2, 1, true)]
        [DataRow(2, 3, true)]
        [DataRow(3, 1, false)]
        [DataRow(3, 2, true)]
        [DataRow(3, 3, false)]
        public void IsNextTo_For_Cardinal(int x, int y, bool expectedResult)
        {
            // Arrange
            var point1 = new Point(2, 2);
            var point2 = new Point(x, y);

            // Act
            var result = point1.IsNextTo(point2, AdjacencyRule.Cardinals);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [DataRow(2, 2, false)]
        [DataRow(0, 0, false)]
        [DataRow(0, 1, false)]
        [DataRow(0, 2, false)]
        [DataRow(0, 3, false)]
        [DataRow(0, 4, false)]
        [DataRow(4, 0, false)]
        [DataRow(4, 1, false)]
        [DataRow(4, 2, false)]
        [DataRow(4, 3, false)]
        [DataRow(4, 4, false)]
        [DataRow(2, 0, false)]
        [DataRow(2, 4, false)]
        [DataRow(1, 1, true)]
        [DataRow(1, 2, false)]
        [DataRow(1, 3, true)]
        [DataRow(2, 1, false)]
        [DataRow(2, 3, false)]
        [DataRow(3, 1, true)]
        [DataRow(3, 2, false)]
        [DataRow(3, 3, true)]
        public void IsNextTo_For_Diagonal(int x, int y, bool expectedResult)
        {
            // Arrange
            var point1 = new Point(2, 2);
            var point2 = new Point(x, y);

            // Act
            var result = point1.IsNextTo(point2, AdjacencyRule.Diagonals);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
