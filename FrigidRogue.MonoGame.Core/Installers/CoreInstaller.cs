using System.Reflection;
using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Configuration;
using FrigidRogue.MonoGame.Core.ConsoleCommands;
using FrigidRogue.MonoGame.Core.Graphics;
using FrigidRogue.MonoGame.Core.Graphics.Quads;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.ConsoleCommands;
using FrigidRogue.MonoGame.Core.Interfaces.Graphics;
using FrigidRogue.MonoGame.Core.Interfaces.Services;
using FrigidRogue.MonoGame.Core.Interfaces.UserInterface;
using FrigidRogue.MonoGame.Core.Services;
using FrigidRogue.MonoGame.Core.UserInterface;
using InputHandlers.Keyboard;
using InputHandlers.Mouse;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Serilog.Core;

namespace FrigidRogue.MonoGame.Core.Installers
{
    public class CoreInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration = null)
        {
            Logger loggerConfig;
            
            if (configuration != null)
            {
                loggerConfig = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
            }
            else
            {
                loggerConfig = new LoggerConfiguration()
                    .WriteTo.File($"{Assembly.GetEntryAssembly()?.GetName().Name ?? "Game"}.log")
                    .MinimumLevel.Debug()
                    .CreateLogger();
            }

            new RequestHandlerContributor().Process(services, typeof(CoreInstaller).Assembly);
            new NotificationHandlerContributor().Process(services, typeof(CoreInstaller).Assembly);

            new MediatorInstaller().Install(services);

            services.AddSingleton<ILogger>(loggerConfig);
            services.AddTransient<IGameTimeService, GameTimeService>();
            services.AddTransient<IGameTurnService, GameTurnService>();
            services.AddTransient<ISaveGameService, SaveGameService>();
            services.AddTransient<ISaveGameFileWriter, SaveGameFileWriter>();
            services.AddTransient<IGameInputService, GameInputService>();
            services.AddTransient<IStopwatchProvider, StopwatchProvider>();
            services.AddTransient<IKeyboardInput, KeyboardInput>();
            services.AddTransient<IMouseInput, MouseInput>();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IGameProvider, GameProvider>();
            services.AddTransient<IGameOptionsStore, GameOptionsStore>();
            services.AddTransient<MaterialQuadTemplate>();
            services.AddTransient<TexturedQuadTemplate>();
            services.AddTransient<IConfigurationSettings, ConfigurationSettings>();
            services.AddTransient<IConsoleCommandServiceFactory, ConsoleCommandServiceFactory>();
            services.AddTransient<IGraphicsSettings>(_ => BaseConfigurationSectionHandler.Load<GraphicsSettings>());
            services.AddTransient<IActionMap, ActionMap>();
            services.TryAddTransient<IActionMapStore, EmptyActionMapStore>();
            services.AddTransient<ISceneGraph, SceneGraph>();
        }
    }
}
