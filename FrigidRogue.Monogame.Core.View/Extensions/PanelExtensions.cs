using GeonBit.UI.Entities;

using Microsoft.Xna.Framework;

namespace FrigidRogue.Monogame.Core.View.Extensions
{
    public static class PanelExtensions
    {
        public static Panel TopPaddingPanel(this Panel panel)
        {
            panel.Skin = PanelSkin.None;
            panel.Size = new Vector2(-1f, 25f);
            panel.Padding = Vector2.Zero;
            panel.Anchor = Anchor.AutoCenter;

            return panel;
        }

        public static Panel TopPaddingPanelWithoutBorder(this Panel panel)
        {
            panel.Skin = PanelSkin.None;
            panel.Size = new Vector2(-1f, 10f);
            panel.Padding = Vector2.Zero;
            panel.Anchor = Anchor.AutoCenter;

            return panel;
        }
    }
}
