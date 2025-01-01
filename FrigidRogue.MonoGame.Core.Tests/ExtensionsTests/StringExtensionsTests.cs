using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.TestInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrigidRogue.MonoGame.Core.Tests.Extensions
{
    [TestClass]
    public class StringExtensionsTests : BaseTest
    {
        [DataTestMethod]
        [DataRow("Test 1", "Test1")]
        [DataRow("Test  1", "Test1")]
        [DataRow(" Test1 ", "Test1")]
        [DataRow(" Te st  1 ", "Test1")]
        [DataRow("", "")]
        [DataRow(" ", "")]
        [DataRow("  ", "")]
        public void Should_Remove_Spaces(String testString, string expectedResult)
        {
            // Act
            var result = testString.RemoveSpaces();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
