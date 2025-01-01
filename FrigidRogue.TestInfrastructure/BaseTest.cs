using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Components.Mediator;
using FrigidRogue.MonoGame.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace FrigidRogue.TestInfrastructure
{
    [TestClass]
    public abstract class BaseTest
    {
        protected FakeLogger FakeLogger;
        protected FakeStopwatchProvider FakeStopwatchProvider;
        protected GameTimeService FakeGameTimeService;

        public BaseTest()
        {
            FakeLogger = new FakeLogger();
            FakeStopwatchProvider = new FakeStopwatchProvider();
            FakeGameTimeService = new GameTimeService(FakeStopwatchProvider);
            SetupBaseComponent(FakeGameTimeService);
            FakeGameTimeService.Start();
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