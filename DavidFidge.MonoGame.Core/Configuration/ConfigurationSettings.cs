using DavidFidge.MonoGame.Core.Interfaces.Services;

namespace DavidFidge.MonoGame.Core.Configuration
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