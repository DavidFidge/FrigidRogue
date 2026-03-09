using FrigidRogue.MonoGame.Core.Components.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace FrigidRogue.MonoGame.Core.Installers
{
    public class MediatorInstaller
    {
        public void Install(IServiceCollection services)
        {
            services.AddTransient<ServiceFactory>(sp => type =>
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return sp.GetServices(type.GetGenericArguments()[0]);
                }

                // If a not found exception is thrown here then check that the handler is registered
                // with a Unit generic parameter
                return sp.GetRequiredService(type);
            });

            services.AddTransient<IMediator, Mediator>();
        }
    }
}
