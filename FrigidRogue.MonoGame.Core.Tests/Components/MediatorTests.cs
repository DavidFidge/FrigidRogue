using FrigidRogue.MonoGame.Core.Components.Mediator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrigidRogue.MonoGame.Core.Tests.Components
{
    [TestClass]
    public class MediatorTests
    {
        [TestMethod]
        public void Send_Should_Call_Request_Handler()
        {
            // Arrange
            var handler = new TestRequestHandler();
            var mediator = new Mediator(type =>
                type == typeof(IRequestHandler<TestRequest>)
                    ? handler
                    : throw new InvalidOperationException());

            // Act
            mediator.Send(new TestRequest());

            // Assert
            Assert.AreEqual(1, handler.HandleCount);
        }

        [TestMethod]
        public void Publish_Should_Call_All_Notification_Handlers()
        {
            // Arrange
            var firstHandler = new TestNotificationHandler();
            var secondHandler = new TestNotificationHandler();
            var handlers = new INotificationHandler<TestNotification>[] { firstHandler, secondHandler };

            var mediator = new Mediator(type =>
                type == typeof(IEnumerable<INotificationHandler<TestNotification>>)
                    ? handlers
                    : throw new InvalidOperationException());

            // Act
            mediator.Publish(new TestNotification());

            // Assert
            Assert.AreEqual(1, firstHandler.HandleCount);
            Assert.AreEqual(1, secondHandler.HandleCount);
        }

        private class TestRequest : IRequest
        {
        }

        private class TestNotification : INotification
        {
        }

        private class TestRequestHandler : IRequestHandler<TestRequest>
        {
            public int HandleCount { get; private set; }

            public void Handle(TestRequest request)
            {
                HandleCount++;
            }
        }

        private class TestNotificationHandler : INotificationHandler<TestNotification>
        {
            public int HandleCount { get; private set; }

            public void Handle(TestNotification request)
            {
                HandleCount++;
            }
        }
    }
}
