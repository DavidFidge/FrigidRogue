using FrigidRogue.MonoGame.Core.Components;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

using Serilog;

namespace FrigidRogue.TestInfrastructure
{
    [TestClass]
    public abstract class BaseTest
    {
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
            baseComponent.Logger = Substitute.For<ILogger>();

            return baseComponent;
        }
    }
}