using System.Reflection;
using FrigidRogue.MonoGame.Core.View.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FrigidRogue.MonoGame.Core.View.Installers
{
    public class ScreenContributor
    {
        public void Process(IServiceCollection services, Assembly assembly)
        {
            foreach (var implementationType in GetLoadableTypes(assembly).Where(t =>
                         t is { IsClass: true, IsAbstract: false } &&
                         typeof(Screen).IsAssignableFrom(t)))
            {
                services.AddTransient(typeof(IScreen), implementationType);
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
