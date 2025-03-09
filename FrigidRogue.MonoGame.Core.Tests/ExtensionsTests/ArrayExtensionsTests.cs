using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.TestInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Tests.Services
{
    [TestClass]
    public class ArrayExtensionsTests : BaseTest
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }

        [TestMethod]
        public void CopyInto_Test_Smaller()
        {
            // Arrange

            var source =
                ".." +
                "..";
            
            var dest =
                "###" +
                "###" +
                "###";

            var sourceArray = source.ToCharArray();
            var destArray = dest.ToCharArray();
            
            // Act
            sourceArray.CopyInto(destArray, 2, 3);
            
            // Assert
            
            var expected =
                "..#" +
                "..#" +
                "###";
            
            CollectionAssert.AreEqual(expected.ToCharArray(), destArray);
        }

        [TestMethod]
        public void CopyInto_Test_Bigger()
        {
            // Arrange

            var source =
                "...." +
                "...." +
                "...." +
                "....";
            
            var dest =
                "###" +
                "###" +
                "###";

            var sourceArray = source.ToCharArray();
            var destArray = dest.ToCharArray();
            
            // Act
            sourceArray.CopyInto(destArray, 4, 3);
            
            // Assert
            
            var expected =
                "..." +
                "..." +
                "...";
            
            CollectionAssert.AreEqual(expected.ToCharArray(), destArray);
        }
        
        [TestMethod]
        public void CopyInto_Test_Bigger_Width()
        {
            // Arrange

            var source =
                "...." +
                "....";
            
            var dest =
                "###" +
                "###" +
                "###";

            var sourceArray = source.ToCharArray();
            var destArray = dest.ToCharArray();
            
            // Act
            sourceArray.CopyInto(destArray, 4, 3);
            
            // Assert
            
            var expected =
                "..." +
                "..." +
                "###";
            
            CollectionAssert.AreEqual(expected.ToCharArray(), destArray);
        }
        
        [TestMethod]
        public void CopyInto_Test_Bigger_Height()
        {
            // Arrange
            var source =
                ".." +
                ".." +
                ".." +
                "..";
            
            var dest =
                "###" +
                "###" +
                "###";

            var sourceArray = source.ToCharArray();
            var destArray = dest.ToCharArray();
            
            // Act
            sourceArray.CopyInto(destArray, 2, 3);
            
            // Assert
            
            var expected =
                "..#" +
                "..#" +
                "..#";
            
            CollectionAssert.AreEqual(expected.ToCharArray(), destArray);
        }
    }
}