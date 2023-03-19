using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.TestInfrastructure;
using GoRogue.GameFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;

namespace FrigidRogue.MonoGame.Core.Tests.Extensions
{
    [TestClass]
    public class MapExtensionsTests : BaseTest
    {
        [TestMethod]
        public void Should_Create_Rectangle_Covering_3_Points()
        {
            // Arrange
            var map = new Map(5, 5,1, Distance.Chebyshev);

            // Act
            var result = map.RectangleCoveringPoints(new Point(1, 1), new Point(3, 2), new Point(4, 1));
            
            // Assert
            var expectedResult = new Rectangle(new Point(1, 1), new Point(4, 2));
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestMethod]
        public void Should_Create_Rectangle_Covering_2_Points()
        {
            // Arrange
            var map = new Map(5, 5,1, Distance.Chebyshev);

            // Act
            var result = map.RectangleCoveringPoints(new Point(4, 2), new Point(3, 4));
            
            // Assert
            var expectedResult = new Rectangle(new Point(3, 2), new Point(4, 4));
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestMethod]
        public void Should_Create_Rectangle_Covering_1_Point()
        {
            // Arrange
            var map = new Map(5, 5,1, Distance.Chebyshev);

            // Act
            var result = map.RectangleCoveringPoints(new Point(1, 1));
            
            // Assert
            var expectedResult = new Rectangle(new Point(1, 1), new Point(1, 1));
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestMethod]
        public void Rectangle_Covering_No_Points_Should_Return_Map_Boundary()
        {
            // Arrange
            var map = new Map(5, 5,1, Distance.Chebyshev);

            // Act
            var result = map.RectangleCoveringPoints();
            
            // Assert
            Assert.AreEqual(map.Bounds(), result);
        }
    }
}
