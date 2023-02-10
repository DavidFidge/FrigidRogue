using System.Reflection;

using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

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
using Serilog;
using Serilog.Core;

namespace FrigidRogue.MonoGame.Core.Installers
{
    public class CoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Logger loggerConfig;
            
            if (container.Kernel.HasComponent(typeof(IConfiguration)))
            {
                var configuration = container.Resolve<IConfiguration>();

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

            container.AddFacility<TypedFactoryFacility>();

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));

            container.Kernel.ComponentModelBuilder.AddContributor(new RequestHandlerContributor());
            container.Kernel.ComponentModelBuilder.AddContributor(new NotificationHandlerContributor());

            container.Install(new MediatorInstaller());

            container.Register(
                
                Component.For<ILogger>()
                    .Instance(loggerConfig),

                Component.For<IGameTimeService>()
                    .ImplementedBy<GameTimeService>(),

                Component.For<IGameTurnService>()
                    .ImplementedBy<GameTurnService>(),

                Component.For<IRandom>()
                    .ImplementedBy<Components.Random>(),

                Component.For<ISaveGameService>()
                    .ImplementedBy<SaveGameService>(),

                Component.For<ISaveGameFileWriter>()
                    .ImplementedBy<SaveGameFileWriter>(),

                Component.For<IGameInputService>()
                    .ImplementedBy<GameInputService>(),

                Component.For<IStopwatchProvider>()
                    .ImplementedBy<StopwatchProvider>()
                    .LifeStyle.Transient,

                Component.For<IKeyboardInput>()
                    .ImplementedBy<KeyboardInput>(),

                Component.For<IMouseInput>()
                    .ImplementedBy<MouseInput>(),
                
                Component.For<IDateTimeProvider>()
                    .ImplementedBy<DateTimeProvider>(),
                
                Component.For<IGameProvider>()
                    .ImplementedBy<GameProvider>(),

                Component.For<IGameOptionsStore>()
                    .ImplementedBy<GameOptionsStore>(),

                Component.For<MaterialQuadTemplate>()
                    .LifeStyle.Transient,

                Component.For<TexturedQuadTemplate>()
                    .LifeStyle.Transient,

                Component.For<IConfigurationSettings>()
                    .ImplementedBy<ConfigurationSettings>(),

                Component.For<IConsoleCommandServiceFactory>()
                    .ImplementedBy<ConsoleCommandServiceFactory>(),

                Component.For<IGraphicsSettings>()
                    .UsingFactoryMethod(k => BaseConfigurationSectionHandler.Load<GraphicsSettings>()),

                Component.For<IActionMap>()
                    .ImplementedBy<ActionMap>(),

                Component.For<IActionMapStore>()
                    .ImplementedBy<EmptyActionMapStore>()
                    .IsFallback(),

                Component.For<ISceneGraph>()
                    .ImplementedBy<SceneGraph>()
                    .LifeStyle.Transient
            );
        }
    }
}
