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

        public static T WithScale<T>(this T entity, float scale) where T : Entity
        {
            entity.Scale = scale;
            return entity;
        }

        public static T WithSmallButtonScale<T>(this T entity) where T : Entity
        {
            entity.WithScale(0.7f);
            return entity;
        }

        public static T WithPadding<T>(this T entity, Vector2 padding) where T : Entity
        {
            entity.Padding = padding;
            return entity;
        }

        public static T WithAnchor<T>(this T entity, Anchor anchor) where T : Entity
        {
            entity.Anchor = anchor;
            return entity;
        }

        public static T WithSize<T>(this T entity, Vector2 size) where T : Entity
        {
            entity.Size = size;
            return entity;
        }

        public static T WithOffset<T>(this T entity, Vector2 offset) where T : Entity
        {
            entity.Offset = offset;
            return entity;
        }

        public static T Bold<T>(this T entity) where T : Paragraph
        {
            entity.TextStyle = FontStyle.Bold;
            return entity;
        }

        public static T WithFillColor<T>(this T entity, Color fillColor) where T : Entity
        {
            entity.FillColor = fillColor;
            return entity;
        }

        public static T WithSkin<T>(this T panelBase, PanelSkin panelSkin) where T : PanelBase
        {
            panelBase.Skin = panelSkin;
            return panelBase;
        }

        public static T NoSkin<T>(this T panelBase) where T : PanelBase
        {
            panelBase.WithSkin(PanelSkin.None);
            return panelBase;
        }
        public static T SimpleSkin<T>(this T panelBase) where T : PanelBase
        {
            panelBase.WithSkin(PanelSkin.Simple);
            return panelBase;
        }

        public static T DefaultSkin<T>(this T panelBase) where T : PanelBase
        {
            panelBase.WithSkin(PanelSkin.Default);
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
            entity.Anchor = Anchor.AutoCenter;

            return entity;
        }

        public static T Invisible<T>(this T entity) where T : Entity
        {
            entity.Opacity = 0;

            return entity;
        }

        public static T Opacity70Percent<T>(this T entity) where T : Entity
        {
            entity.Opacity = (int)(256 * 0.7);

            return entity;
        }

        public static T Opacity80Percent<T>(this T entity) where T : Entity
        {
            entity.Opacity = (int)(256 * 0.8);

            return entity;
        }

        public static T Opacity90Percent<T>(this T entity) where T : Entity
        {
            entity.Opacity = (int)(256 * 0.9);

            return entity;
        }

        public static T Opacity100Percent<T>(this T entity) where T : Entity
        {
            entity.Opacity = Byte.MaxValue;

            return entity;
        }
    }
}
