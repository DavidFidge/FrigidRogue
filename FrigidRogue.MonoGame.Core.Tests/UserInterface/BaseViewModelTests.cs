using FrigidRogue.MonoGame.Core.Messages;
using FrigidRogue.MonoGame.Core.UserInterface;
using FrigidRogue.TestInfrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

namespace FrigidRogue.MonoGame.Core.Tests.UserInterface
{
    [TestClass]
    public class BaseViewModelTests : BaseTest
    {
        [TestMethod]
        public void Notify_Should_Send_NotifyViewModelChangedRequest()
        {
            // Arrange
            var testViewModel = new TestViewModel();
            SetupBaseComponent(testViewModel);

            // Act
            testViewModel.TestNotify();

            // Assert
            testViewModel.Mediator.Received().Send(Arg.Any<NotifyViewModelChangedRequest<TestData>>());
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
