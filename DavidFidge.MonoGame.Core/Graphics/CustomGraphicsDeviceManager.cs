using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DavidFidge.MonoGame.Core.Services
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

        public List<DisplayMode> GetSupportedDisplayModes()
        {
            var supportedDisplayModes = new HashSet<DisplayMode>();

            foreach (Microsoft.Xna.Framework.Graphics.DisplayMode displaymode in GraphicsDevice.Adapter.SupportedDisplayModes)
            {
                if (displaymode.Width >= 1000 && displaymode.Height >= 900)
                {
                    var supportedDisplayMode = new DisplayMode(
                        displaymode.Width,
                        displaymode.Height,
                        displaymode.AspectRatio);

                    supportedDisplayModes.Add(supportedDisplayMode);
                }
            }

            return supportedDisplayModes.ToList();
        }

        public void SetDisplayMode(DisplayMode displayMode, bool isFullScreen)
        {
            PreferredBackBufferWidth = displayMode.Width;
            PreferredBackBufferHeight = displayMode.Height;
            IsFullScreen = isFullScreen;

            ApplyChanges();
        }
    }
}