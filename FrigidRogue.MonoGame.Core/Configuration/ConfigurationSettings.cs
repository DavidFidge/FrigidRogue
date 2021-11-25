using FrigidRogue.MonoGame.Core.Interfaces.Services;

namespace FrigidRogue.MonoGame.Core.Configuration
{
    public class ConfigurationSettings : IConfigurationSettings
    {
        public ConfigurationSettings()
        {
            GraphicsSettings = new GraphicsSettings();
        }

        public IGraphicsSettings GraphicsSettings { get; set; }
    }
}