using FrigidRogue.MonoGame.Core.Components;

using MediatR;

namespace FrigidRogue.MonoGame.Core.Messages
{
    public class EntityTransformChanged : INotification
    {
        public EntityTransformChanged(Entity entity)
        {
            Entity = entity;
        }

        public Entity Entity { get; }
    }
}