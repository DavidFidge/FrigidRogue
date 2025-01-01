using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.TestInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;

namespace FrigidRogue.MonoGame.Core.Tests.Extensions
{
    [TestClass]
    public class GridViewExtensionsTests : BaseTest
    {
        [TestMethod]
        public void Should_Get_Subset_Of_Map()
        {
            // Arrange
            var test = new[]
            {
                true, true, true,
                false, true, false,
                false, false, false
            };
            
            var gridView = new ArrayView<bool>(test, 3);
            
            // Act
            var result = gridView.Subset(new Rectangle(1, 1, 2, 2));
            
            // Assert
            var expectedResult = new[]
            {
                true, false,
                false, false
            };

            CollectionAssert.AreEqual(expectedResult, result.ToArray());
        }
    }
}
