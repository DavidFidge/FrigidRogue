using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using DavidFidge.MonoGame.Core.UserInterface;
using DavidFidge.Monogame.Core.View.Interfaces;

using GeonBit.UI.Entities;

namespace DavidFidge.Monogame.Core.View.Installers
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
                    .ImplementedBy<UserInterface>()
            );
        }
    }
}