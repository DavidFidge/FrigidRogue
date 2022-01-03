using System;

using FrigidRogue.MonoGame.Core.Graphics;
using FrigidRogue.MonoGame.Core.Interfaces.Graphics;
using FrigidRogue.MonoGame.Core.Messages;

namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class Entity : BaseComponent, ITransformable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ITransform Transform { get; set; } = new EntityTransform();

        public void TransformChanged()
        {
            Mediator?.Publish(new EntityTransformChangedNotification(this));
        }
    }
}
