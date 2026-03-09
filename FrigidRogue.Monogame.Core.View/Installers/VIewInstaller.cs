using Microsoft.Extensions.DependencyInjection;
using FrigidRogue.MonoGame.Core.UserInterface;
using FrigidRogue.MonoGame.Core.View.Interfaces;
using GeonBit.UI.Entities;

namespace FrigidRogue.MonoGame.Core.View.Installers
{
    public class ViewInstaller
    {
        public void Install(IServiceCollection services)
        {
            new ScreenContributor().Process(services, typeof(ViewInstaller).Assembly);
            
            services.AddTransient<IRootPanel<Entity>, RootGeonBitPanel>();
            services.AddTransient<IUserInterface, GeonBitUserInterfaceWrapper>();
        }
    }
}
