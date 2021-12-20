using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using FrigidRogue.MonoGame.Core.Components;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

using Serilog;

using ILogger = Serilog.ILogger;

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

        protected void SetupBaseComponent(BaseComponent baseComponent)
        {
            baseComponent.Logger = new LoggerConfiguration()
                .WriteTo.Seq("http://localhost:5341/")
                .WriteTo.Console()
                .WriteTo.File($"{Assembly.GetEntryAssembly()?.GetName().Name ?? "GameTests"}.log")
                .MinimumLevel.Debug()
                .CreateLogger();

            baseComponent.Mediator = new NullMediator();
        }

        protected T SetupBaseComponent<T>(T baseComponent)
            where T : BaseComponent
        {
            baseComponent.Mediator = Substitute.For<IMediator>();
            baseComponent.Logger = Substitute.For<ILogger>();

            return baseComponent;
        }
    }

    public class NullMediator : IMediator
    {
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = new CancellationToken())
        {
            return null;
        }

        public Task<object?> Send(object request, CancellationToken cancellationToken = new CancellationToken())
        {
            return null;
        }

        public Task Publish(object notification, CancellationToken cancellationToken = new CancellationToken())
        {
            return Unit.Task;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = new CancellationToken()) where TNotification : INotification
        {
            return Unit.Task;
        }
    }
}