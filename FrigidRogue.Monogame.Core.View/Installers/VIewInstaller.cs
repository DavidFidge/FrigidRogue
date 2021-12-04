using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using FrigidRogue.MonoGame.Core.UserInterface;
using FrigidRogue.MonoGame.Core.View.Interfaces;

using GeonBit.UI.Entities;

namespace FrigidRogue.MonoGame.Core.View.Installers
{
    public class ViewInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IRootPanel<Entity>>()
                    .ImplementedBy<RootGeonBitPanel>()
                    .LifeStyle.Transient,

                Component.For<IUserInterface>()
                    .ImplementedBy<GeonBitUserInterfaceWrapper>()
            );
        }
    }
}