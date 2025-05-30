using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FrigidRogue.MonoGame.Core.Components.Mediator;

namespace FrigidRogue.MonoGame.Core.Installers
{
    public class MediatorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IMediator>().ImplementedBy<Mediator>());

            container.Register(Component.For<ServiceFactory>().UsingFactoryMethod<ServiceFactory>(k => (type =>
            {
                var enumerableType = type
                    .GetInterfaces()
                    .Concat(new[] { type })
                    .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));

                // If a not found exception is thrown here then check that the handler is registered
                // with a Unit generic parameter
                return enumerableType != null ? k.ResolveAll(enumerableType.GetGenericArguments()[0]) : k.Resolve(type);
            })));
        }
    }
}
