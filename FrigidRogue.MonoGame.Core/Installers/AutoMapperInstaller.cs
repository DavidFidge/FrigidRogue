using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FrigidRogue.MonoGame.Core.Installers
{
    public class AutoMapperInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Register IMapper with registered IConfigurationProvider
            container.Register(
                Component.For<IMapper>().UsingFactoryMethod(kernel =>
                    new Mapper(kernel.Resolve<IConfigurationProvider>(), kernel.Resolve)));

            // Register IConfigurationProvider with all registered profiles
            container.Register(Component.For<IConfigurationProvider>().UsingFactoryMethod(kernel =>
            {
                return new MapperConfiguration(configuration =>
                {
                    kernel.ResolveAll<Profile>().ToList().ForEach(configuration.AddProfile);
                });
            }).LifestyleSingleton());
        }
    }
}
