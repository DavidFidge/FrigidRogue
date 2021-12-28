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

        public static T WithSmallButtonScale<T>(this T entity) where T : Entity
        {
            entity.Scale = 0.7f;
            return entity;
        }

        public static T WithPadding<T>(this T entity, Vector2 padding) where T : Entity
        {
            entity.Padding = padding;
            return entity;
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
            entity.Size = new Vector2(0.95f, entity.Size.Y);

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
