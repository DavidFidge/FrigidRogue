using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class CustomGraphicsDeviceManager : GraphicsDeviceManager
    {
        public bool IsVerticalSync { get; set; }

        public CustomGraphicsDeviceManager(Game game)
            : base(game)
        {
            PreparingDeviceSettings += OnPreparingDeviceSettings;
        }

        protected void OnPreparingDeviceSettings(Object sender, PreparingDeviceSettingsEventArgs args)
        {
            if (args.GraphicsDeviceInformation.PresentationParameters.IsFullScreen)
            {
                args.GraphicsDeviceInformation.PresentationParameters.PresentationInterval = IsVerticalSync
                    ? PresentInterval.Default
                    : PresentInterval.Immediate;
            }

            args.GraphicsDeviceInformation.GraphicsProfile = GraphicsProfile.HiDef;
        }

        public List<DisplayDimension> GetSupportedDisplayModes()
        {
            var supportedDisplayModes = new HashSet<DisplayDimension>();

            foreach (var displayMode in GraphicsDevice.Adapter.SupportedDisplayModes)
            {
                if (displayMode.Width >= 1000 && displayMode.Height >= 900)
                {
                    var supportedDisplayMode = new DisplayDimension(
                        displayMode.Width,
                        displayMode.Height,
                        displayMode.AspectRatio);

                    supportedDisplayModes.Add(supportedDisplayMode);
                }
            }

            return supportedDisplayModes.ToList();
        }

        public void SetDisplayMode(DisplaySettings displaySettings)
        {
            PreferredBackBufferWidth = displaySettings.DisplayDimension.Width;
            PreferredBackBufferHeight = displaySettings.DisplayDimension.Height;

            IsFullScreen = displaySettings.IsFullScreen;

            HardwareModeSwitch = displaySettings.IsFullScreen && !displaySettings.IsBorderlessWindowed;
            IsVerticalSync = displaySettings.IsVerticalSync;

            ApplyChanges();
        }
    }
}
