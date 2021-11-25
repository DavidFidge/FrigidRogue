using DavidFidge.MonoGame.Core.Messages;
using DavidFidge.MonoGame.Core.UserInterface;
using DavidFidge.TestInfrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

namespace DavidFidge.MonoGame.Core.Tests.UserInterface
{
    [TestClass]
    public class BaseViewModelTests : BaseTest
    {
        [TestMethod]
        public void Notify_Should_Send_UpdateViewRequest()
        {
            // Arrange
            var testViewModel = new TestViewModel();
            SetupBaseComponent(testViewModel);

            // Act
            testViewModel.TestNotify();

            // Assert
            testViewModel.Mediator.Received().Send(Arg.Any<UpdateViewRequest<TestData>>());
        }

        public class TestData
        {
        }

        public class TestViewModel : BaseViewModel<TestData>
        {
            public void TestNotify()
            {
                Notify();
            }
        }
    }
}
