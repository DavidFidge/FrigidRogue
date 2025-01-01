using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.View.Extensions
{
    public static class PanelBaseExtensions
    {
        public static T TopPaddingPanel<T>(this T panelBase) where T : PanelBase
        {
            panelBase.Skin = PanelSkin.None;
            panelBase.Size = new Vector2(-1f, 25f);
            panelBase.Padding = Vector2.Zero;
            panelBase.Anchor = Anchor.AutoCenter;

            return panelBase;
        }

        public static T TopPaddingPanelWithoutBorder<T>(this T entity) where T : PanelBase
        {
            entity.Skin = PanelSkin.None;
            entity.Size = new Vector2(-1f, 10f);
            entity.Padding = Vector2.Zero;
            entity.Anchor = Anchor.AutoCenter;

            return entity;
        }

        public static T AutoHeight<T>(this T panelBase) where T : PanelBase
        {
            panelBase.AdjustHeightAutomatically = true;
            panelBase.Height(0);

            return panelBase;
        }
    }
}
