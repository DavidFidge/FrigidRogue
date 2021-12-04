using FrigidRogue.MonoGame.Core.Extensions;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class DisplaySettings
    {
        public DisplayDimensions DisplayDimensions { get; }
        public bool IsFullScreen { get; }
        public bool IsVerticalSync { get; }
        public bool IsBorderlessWindowed { get; }

        public DisplaySettings(DisplayDimensions displayDimensions, bool isFullScreen, bool isVerticalSync, bool isBorderlessWindowed)
        {
            DisplayDimensions = displayDimensions;
            IsFullScreen = isFullScreen;
            IsVerticalSync = isVerticalSync;
            IsBorderlessWindowed = isBorderlessWindowed;
        }

        public override string ToString()
        {
            return this.PropertiesToString();
        }
    }
}