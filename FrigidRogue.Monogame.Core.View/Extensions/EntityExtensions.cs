using System;
using GeonBit.UI.Entities;

using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.View.Extensions
{
    public static class EntityExtensions
    {
        public static T ForceDirty<T>(this T entity) where T : Entity
        {
            var oldSize = entity.Size;
            entity.Size = new Vector2(entity.Size.X, float.MaxValue);
            entity.Size = oldSize;

            return entity;
        }

        public static T Scale<T>(this T entity, float scale) where T : Entity
        {
            entity.Scale = scale;
            return entity;
        }

        public static T WithSmallButtonScale<T>(this T entity) where T : Entity
        {
            entity.Scale(0.7f);
            return entity;
        }

        public static T Padding<T>(this T entity, Vector2 padding) where T : Entity
        {
            entity.Padding = padding;
            return entity;
        }

        public static T Anchor<T>(this T entity, Anchor anchor) where T : Entity
        {
            entity.Anchor = anchor;
            return entity;
        }

        public static T Size<T>(this T entity, Vector2 size) where T : Entity
        {
            entity.Size = size;
            return entity;
        }

        public static T Offset<T>(this T entity, Vector2 offset) where T : Entity
        {
            entity.Offset = offset;
            return entity;
        }

        public static T Bold<T>(this T entity) where T : Paragraph
        {
            entity.TextStyle = FontStyle.Bold;
            return entity;
        }

        public static T FillColor<T>(this T entity, Color fillColor) where T : Entity
        {
            entity.FillColor = fillColor;
            return entity;
        }

        public static T ProgressBarFillColor<T>(this T progressBar, Color fillColor) where T : ProgressBar
        {
            progressBar.ProgressFill.FillColor = fillColor;
            return progressBar;
        }

        public static T StepsCount<T>(this T progressBar, uint stepsCount) where T : ProgressBar
        {
            progressBar.StepsCount = stepsCount;
            return progressBar;
        }

        public static T TransparentFillColor<T>(this T entity) where T : Entity
        {
            entity.FillColor = new Color(new Color(), 0);
            return entity;
        }

        public static T Skin<T>(this T panelBase, PanelSkin panelSkin) where T : PanelBase
        {
            panelBase.Skin = panelSkin;
            return panelBase;
        }

        public static T NoSkin<T>(this T panelBase) where T : PanelBase
        {
            panelBase.Skin(PanelSkin.None);
            return panelBase;
        }
        public static T SimpleSkin<T>(this T panelBase) where T : PanelBase
        {
            panelBase.Skin(PanelSkin.Simple);
            return panelBase;
        }

        public static T DefaultSkin<T>(this T panelBase) where T : PanelBase
        {
            panelBase.Skin(PanelSkin.Default);
            return panelBase;
        }

        public static T AlternativeSkin<T>(this T panelBase) where T : PanelBase
        {
            panelBase.Skin(PanelSkin.Alternative);
            return panelBase;
        }

        public static T FancySkin<T>(this T panelBase) where T : PanelBase
        {
            panelBase.Skin(PanelSkin.Fancy);
            return panelBase;
        }

        public static T ListBackgroundSkin<T>(this T panelBase) where T : PanelBase
        {
            panelBase.Skin(PanelSkin.ListBackground);
            return panelBase;
        }

        public static T NoPadding<T>(this T entity) where T : Entity
        {
            entity.Padding = Vector2.Zero;
            return entity;
        }

        public static T Hidden<T>(this T entity) where T : Entity
        {
            entity.Visible = false;
            return entity;
        }

        public static T Visible<T>(this T entity) where T : Entity
        {
            entity.Visible = true;
            return entity;
        }

        public static T WithParent<T>(this T entity, Entity parent) where T : Entity
        {
            parent.AddChild(entity);
            return entity;
        }

        public static T WrappedInPanel<T>(
            this T entity,
            Anchor panelAnchor,
            Vector2 size,
            Entity panelParent) where T : Entity
        {
            var panel = new Panel(
                size,
                PanelSkin.None,
                panelAnchor)
                .NoPadding();

            panel.AddChild(entity);

            panelParent.AddChild(panel);

            return entity;
        }


        public static T WidthOfButton<T>(this T entity) where T : Entity
        {
            entity.Size = Button.DefaultStyle.GetStyleProperty("DefaultSize").asVector;

            return entity;
        }

        public static T WidthOfButtonWithPadding<T>(this T entity) where T : Entity
        {
            entity.Size = Button.DefaultStyle.GetStyleProperty("DefaultSize").asVector + Entity.DefaultStyle.GetStyleProperty("Padding").asVector * 2;

            return entity;
        }

        public static T WidthOfScreen<T>(this T entity) where T : Entity
        {
            entity.Size = new Vector2(-1, entity.Size.Y);

            return entity;
        }

        public static T Width<T>(this T entity, float width) where T : Entity
        {
            entity.Size = new Vector2(width, entity.Size.Y);

            return entity;
        }

        public static T Height<T>(this T entity, float height) where T : Entity
        {
            entity.Size = new Vector2(entity.Size.X, height);

            return entity;
        }

        public static T WidthOfButton<T>(this T entity, float height) where T : Entity
        {
            entity.Size = new Vector2(Button.DefaultStyle.GetStyleProperty("DefaultSize").asVector.X, height);

            return entity;
        }

        public static T WidthOfButtonWithPadding<T>(this T entity, float height) where T : Entity
        {
            entity.Size = new Vector2((Button.DefaultStyle.GetStyleProperty("DefaultSize").asVector + Entity.DefaultStyle.GetStyleProperty("Padding").asVector * 2).X, height);

            return entity;
        }

        public static T Centred<T>(this T entity) where T : Entity
        {
            entity.Anchor = GeonBit.UI.Entities.Anchor.AutoCenter;

            return entity;
        }

        public static T Invisible<T>(this T entity) where T : Entity
        {
            entity.Opacity = 0;

            return entity;
        }

        public static T OpacityPercent<T>(this T entity, uint percent) where T : Entity
        {
            if (percent > 100)
                percent = 100;

            entity.Opacity = Convert.ToByte(256 * percent / 100);

            return entity;
        }

        public static T SolidOpacity<T>(this T entity) where T : Entity
        {
            entity.Opacity = Byte.MaxValue;

            return entity;
        }

        public static T Locked<T>(this T entity) where T : Entity
        {
            entity.Locked = true;

            return entity;
        }
    }
}
