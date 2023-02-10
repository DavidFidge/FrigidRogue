using FrigidRogue.MonoGame.Core.Components;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

using ILogger = Serilog.ILogger;

namespace FrigidRogue.TestInfrastructure
{
    [TestClass]
    public abstract class BaseTest
    {
        protected FakeLogger FakeLogger;

        public BaseTest()
        {
            FakeLogger = new FakeLogger();
        }

        [TestInitialize]
        public virtual void Setup()
        {
        }

        [TestCleanup]
        public virtual void TearDown()
        {
        }

        protected T SetupBaseComponent<T>(T baseComponent)
            where T : BaseComponent
        {
            baseComponent.Mediator = Substitute.For<IMediator>();
            baseComponent.Logger = FakeLogger;

            return baseComponent;
        }
    }
}