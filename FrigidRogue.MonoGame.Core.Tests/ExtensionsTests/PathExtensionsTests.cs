using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.TestInfrastructure;
using GoRogue.GameFramework;
using GoRogue.Pathing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;

namespace FrigidRogue.MonoGame.Core.Tests.Extensions
{
    [TestClass]
    public class PathExtensionsTests : BaseTest
    {
        [TestMethod]
        public void GetStepAfterPoint_Should_Exclude_First_Step()
        {
            // Arrange
            var steps = new[]
            {
                new Point(1, 1),
                new Point(2, 2),
                new Point(3, 3)
            };
            
            var path = new Path(steps);

            // Act
            var result = path.GetStepAfterPoint(new Point(1, 1));
            
            // Assert
            Assert.AreEqual(Point.None, result);
        }
        
        [TestMethod]
        public void GetStepAfterPoint_Should_Get_Step_After_Given_Point()
        {
            // Arrange
            var steps = new[]
            {
                new Point(1, 1),
                new Point(2, 2),
                new Point(3, 3)
            };
            
            var path = new Path(steps);

            // Act
            var result = path.GetStepAfterPoint(new Point(2, 2));
            
            // Assert
            Assert.AreEqual(new Point(3, 3), result);
        }
        
        [TestMethod]
        public void GetStepAfterPoint_Should_Return_None_If_No_Step_After_Given_Point_1()
        {
            // Arrange
            var steps = new[]
            {
                new Point(1, 1),
                new Point(2, 2),
                new Point(3, 3)
            };
            
            var path = new Path(steps);

            // Act
            var result = path.GetStepAfterPoint(new Point(3, 3));
            
            // Assert
            Assert.AreEqual(Point.None, result);
        }
        
        [TestMethod]
        public void GetStepAfterPoint_Should_Return_None_If_No_Step_After_Given_Point_2()
        {
            // Arrange
            var steps = new[]
            {
                new Point(1, 1)
            };
            
            var path = new Path(steps);

            // Act
            var result = path.GetStepAfterPoint(new Point(1, 1));
            
            // Assert
            Assert.AreEqual(Point.None, result);
        }
        
        [TestMethod]
        public void GetStepAfterPoint_Should_Return_None_If_No_Steps_In_Path()
        {
            // Arrange
            var steps = new Point[]
            {
            };
            
            var path = new Path(steps);

            // Act
            var result = path.GetStepAfterPoint(new Point(0, 0));
            
            // Assert
            Assert.AreEqual(Point.None, result);
        }
        
        [TestMethod]
        public void GetStepAfterPoint_Should_Return_None_If_Given_Point_Not_In_List_Of_Steps()
        {
            // Arrange
            var steps = new[]
            {
                new Point(1, 1),
                new Point(2, 2),
                new Point(3, 3)
            };
            
            var path = new Path(steps);

            // Act
            var result = path.GetStepAfterPoint(new Point(4, 4));
            
            // Assert
            Assert.AreEqual(Point.None, result);
        }
        
        [TestMethod]
        public void GetStepAfterPointWithStart_Should_Get_Step_After_Given_Point_1()
        {
            // Arrange
            var steps = new[]
            {
                new Point(1, 1),
                new Point(2, 2),
                new Point(3, 3)
            };
            
            var path = new Path(steps);

            // Act
            var result = path.GetStepAfterPointWithStart(new Point(1, 1));
            
            // Assert
            Assert.AreEqual(new Point(2, 2), result);
        }
        
        [TestMethod]
        public void GetStepAfterPointWithStart_Should_Get_Step_After_Given_Point_2()
        {
            // Arrange
            var steps = new[]
            {
                new Point(1, 1),
                new Point(2, 2),
                new Point(3, 3)
            };
            
            var path = new Path(steps);

            // Act
            var result = path.GetStepAfterPointWithStart(new Point(2, 2));
            
            // Assert
            Assert.AreEqual(new Point(3, 3), result);
        }
        
        [TestMethod]
        public void GetStepAfterPointWithStart_Should_Return_None_If_No_Step_After_Given_Point_1()
        {
            // Arrange
            var steps = new[]
            {
                new Point(1, 1),
                new Point(2, 2),
                new Point(3, 3)
            };
            
            var path = new Path(steps);

            // Act
            var result = path.GetStepAfterPointWithStart(new Point(3, 3));
            
            // Assert
            Assert.AreEqual(Point.None, result);
        }
        
        [TestMethod]
        public void GetStepAfterPointWithStart_Should_Return_None_If_No_Step_After_Given_Point_2()
        {
            // Arrange
            var steps = new[]
            {
                new Point(1, 1)
            };
            
            var path = new Path(steps);

            // Act
            var result = path.GetStepAfterPointWithStart(new Point(1, 1));
            
            // Assert
            Assert.AreEqual(Point.None, result);
        }
        
        [TestMethod]
        public void GetStepAfterPointWithStart_Should_Return_None_If_No_Steps_In_Path()
        {
            // Arrange
            var steps = new Point[]
            {
            };
            
            var path = new Path(steps);

            // Act
            var result = path.GetStepAfterPointWithStart(new Point(0, 0));
            
            // Assert
            Assert.AreEqual(Point.None, result);
        }
        
        [TestMethod]
        public void GetStepAfterPointWithStart_Should_Return_None_If_Given_Point_Not_In_List_Of_Steps()
        {
            // Arrange
            var steps = new[]
            {
                new Point(1, 1),
                new Point(2, 2),
                new Point(3, 3)
            };
            
            var path = new Path(steps);

            // Act
            var result = path.GetStepAfterPointWithStart(new Point(4, 4));
            
            // Assert
            Assert.AreEqual(Point.None, result);
        }       
    }
}
