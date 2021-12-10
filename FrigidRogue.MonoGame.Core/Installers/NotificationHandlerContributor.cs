using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;

namespace FrigidRogue.MonoGame.Core.Installers
{
    public class NotificationHandlerContributor : IContributeComponentModelConstruction
    {
        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            foreach (var interfaceType in model.Implementation.GetInterfaces())
            {
                if (interfaceType.Name.StartsWith("INotificationHandler") && interfaceType.GetGenericArguments().Length > 0)
                {
                    model.AddService(interfaceType);
                }
            }
        }
    }
}