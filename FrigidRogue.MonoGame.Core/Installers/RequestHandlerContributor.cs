using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FrigidRogue.MonoGame.Core.Installers
{
    public class RequestHandlerContributor
    {
        public void Process(IServiceCollection services, Assembly assembly)
        {
            foreach (var implementationType in GetLoadableTypes(assembly).Where(t => t is { IsClass: true, IsAbstract: false }))
            {
                foreach (var interfaceType in implementationType.GetInterfaces())
                {
                    if (interfaceType.Name.StartsWith("IRequestHandler") && interfaceType.GetGenericArguments().Length > 0)
                    {
                        services.AddTransient(interfaceType, implementationType);
                    }
                }
            }
        }

        private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t != null).Cast<Type>();
            }
        }
    }
}
