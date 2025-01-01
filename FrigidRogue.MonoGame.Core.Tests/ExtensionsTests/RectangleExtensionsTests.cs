using FrigidRogue.TestInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SadRogue.Primitives;
using RectangleExtensions = FrigidRogue.MonoGame.Core.Extensions.RectangleExtensions;

namespace FrigidRogue.MonoGame.Core.Tests.Extensions
{
    [TestClass]
    public class RectangleExtensionsTests : BaseTest
    {
        [DataTestMethod]
        [DataRow(1, 1, 3, 2, 4, 1, 1, 1, 4, 2)]
        [DataRow(4, 1, 3, 2, 1, 1, 1, 1, 4, 2)]
        [DataRow(3, 2, 4, 1, 1, 1, 1, 1, 4, 2)]
        [DataRow(1, 1, 4, 1, 3, 2, 1, 1, 4, 2)]
        public void Should_Create_Rectangle_Covering_3_Points(int x1, int y1, int x2, int y2, int x3, int y3, int minXResult, int minYResult, int maxXResult, int maxYResult)
        {
            // Act
            var result = RectangleExtensions.CoveringPoints(new Point(x1, y1), new Point(x2, y2), new Point(x3, y3));
            
            // Assert
            var expectedResult = new Rectangle(new Point(minXResult, minYResult), new Point(maxXResult, maxYResult));
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [DataTestMethod]
        [DataRow(5, 5, 9, 9, 5, 5, 9, 9)]
        [DataRow(5, 5, 9, 1, 5, 1, 9, 5)]
        [DataRow(5, 5, 1, 1, 1, 1, 5, 5)]
        [DataRow(5, 5, 1, 9, 1, 5, 5, 9)]
        public void Should_Create_Rectangle_Covering_2_Points(int x1, int y1, int x2, int y2, int minXResult, int minYResult, int maxXResult, int maxYResult)
        {
            // Act
            var result = RectangleExtensions.CoveringPoints(new Point(x1, y1), new Point(x2, y2));
            
            // Assert
            var expectedResult = new Rectangle(new Point(minXResult, minYResult), new Point(maxXResult, maxYResult));
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestMethod]
        public void Should_Create_Rectangle_Covering_1_Point()
        {
            // Act
            var result = RectangleExtensions.CoveringPoints(new Point(1, 1));
            
            // Assert
            var expectedResult = new Rectangle(new Point(1, 1), new Point(1, 1));
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestMethod]
        public void Create_Rectangle_Covering_No_Points_Should_Return_Empty_Rectangle()
        {
            // Act
            var result = RectangleExtensions.CoveringPoints();
            
            // Assert
            Assert.AreEqual(Rectangle.Empty, result);
        }
        
        [DataTestMethod]
        [DataRow(5, 5, 9, 9, 5, 5, 9, 9)]
        [DataRow(5, 5, 9, 1, 5, 1, 9, 5)]
        [DataRow(5, 5, 1, 1, 1, 1, 5, 5)]
        [DataRow(5, 5, 1, 9, 1, 5, 5, 9)]
        public void Should_Create_Rectangle_WithExtents_Unordered(int x1, int y1, int x2, int y2, int minXResult, int minYResult, int maxXResult, int maxYResult)
        {
            // Act
            var result = RectangleExtensions.WithExtentsUnordered(new Point(x1, y1), new Point(x2, y2));
            
            // Assert
            var expectedResult = new Rectangle(new Point(minXResult, minYResult), new Point(maxXResult, maxYResult));
            
            Assert.AreEqual(expectedResult, result);
        }
    }
}
