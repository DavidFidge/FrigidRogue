using GeonBit.UI.Entities;

using Microsoft.Xna.Framework;

namespace DavidFidge.Monogame.Core.View.Extensions
{
    public static class EntityExtensions
    {
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
    }
}
