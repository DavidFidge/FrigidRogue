using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using FrigidRogue.MonoGame.Core.View.Interfaces;

namespace FrigidRogue.MonoGame.Core.View.Installers
{
    public class ScreenContributor : IContributeComponentModelConstruction
    {
        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            if (typeof(Screen).IsAssignableFrom(model.Implementation))
                model.AddService(typeof(IScreen));
        }
    }
}