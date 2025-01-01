using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Components.Mediator;

namespace FrigidRogue.MonoGame.Core.Messages
{
    public class EntityTransformChangedNotification : INotification
    {
        public EntityTransformChangedNotification(Entity entity)
        {
            Entity = entity;
        }

        public Entity Entity { get; }
    }
}