using DavidFidge.MonoGame.Core.Components;

using MediatR;

namespace DavidFidge.MonoGame.Core.Messages
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