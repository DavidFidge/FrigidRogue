using System;
using GeonBit.UI.Entities;

using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.View.Extensions
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

        public static Panel WidthOfButton(this Panel panel)
        {
            panel.Size = Button.DefaultStyle.GetStyleProperty("DefaultSize").asVector + Entity.DefaultStyle.GetStyleProperty("Padding").asVector * 2;

            return panel;
        }

        public static Panel AutoHeight(this Panel panel)
        {
            panel.AdjustHeightAutomatically = true;

            return panel;
        }

        public static Panel Opacity70Percent(this Panel panel)
        {
            panel.Opacity = (int)(256 * 0.7);

            return panel;
        }

        public static Panel Opacity80Percent(this Panel panel)
        {
            panel.Opacity = (int)(256 * 0.8);

            return panel;
        }

        public static Panel Opacity90Percent(this Panel panel)
        {
            panel.Opacity = (int)(256 * 0.9);

            return panel;
        }

        public static Panel Opacity100Percent(this Panel panel)
        {
            panel.Opacity = Byte.MaxValue;

            return panel;
        }
    }
}
