using System;
using GeonBit.UI.Entities;

using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.View.Extensions
{
    public static class GeonBitExtensions
    {
        public static T TopPaddingPanel<T>(this T panel) where T : PanelBase
        {
            panel.Skin = PanelSkin.None;
            panel.Size = new Vector2(-1f, 25f);
            panel.Padding = Vector2.Zero;
            panel.Anchor = Anchor.AutoCenter;

            return panel;
        }

        public static T TopPaddingPanelWithoutBorder<T>(this T panel) where T : PanelBase
        {
            panel.Skin = PanelSkin.None;
            panel.Size = new Vector2(-1f, 10f);
            panel.Padding = Vector2.Zero;
            panel.Anchor = Anchor.AutoCenter;

            return panel;
        }

        public static T WidthOfButton<T>(this T panel) where T : Entity
        {
            panel.Size = Button.DefaultStyle.GetStyleProperty("DefaultSize").asVector;

            return panel;
        }

        public static T WidthOfButtonWithPadding<T>(this T panel) where T : Entity
        {
            panel.Size = Button.DefaultStyle.GetStyleProperty("DefaultSize").asVector + Entity.DefaultStyle.GetStyleProperty("Padding").asVector * 2;

            return panel;
        }

        public static T WidthOfButton<T>(this T panel, float height) where T : Entity
        {
            panel.Size = new Vector2(Button.DefaultStyle.GetStyleProperty("DefaultSize").asVector.X, height);

            return panel;
        }

        public static T WidthOfButtonWithPadding<T>(this T panel, float height) where T : Entity
        {
            panel.Size = new Vector2((Button.DefaultStyle.GetStyleProperty("DefaultSize").asVector + Entity.DefaultStyle.GetStyleProperty("Padding").asVector * 2).X, height);

            return panel;
        }

        public static T AutoHeight<T>(this T panel) where T : PanelBase
        {
            panel.AdjustHeightAutomatically = true;

            return panel;
        }

        public static T Invisible<T>(this T panel) where T : Entity
        {
            panel.Opacity = 0;

            return panel;
        }

        public static T Opacity70Percent<T>(this T panel) where T : Entity
        {
            panel.Opacity = (int)(256 * 0.7);

            return panel;
        }

        public static T Opacity80Percent<T>(this T panel) where T : Entity
        {
            panel.Opacity = (int)(256 * 0.8);

            return panel;
        }

        public static T Opacity90Percent<T>(this T panel) where T : Entity
        {
            panel.Opacity = (int)(256 * 0.9);

            return panel;
        }

        public static T Opacity100Percent<T>(this T panel) where T : Entity
        {
            panel.Opacity = Byte.MaxValue;

            return panel;
        }
    }
}
