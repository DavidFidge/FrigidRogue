using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;

namespace FrigidRogue.MonoGame.Core.Installers
{
    public class RequestHandlerContributor : IContributeComponentModelConstruction
    {
        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            foreach (var interfaceType in model.Implementation.GetInterfaces())
            {
                if (interfaceType.Name.StartsWith("IRequestHandler") && interfaceType.GetGenericArguments().Length > 0)
                {
                    model.AddService(interfaceType);
                }
            }
        }
    }
}