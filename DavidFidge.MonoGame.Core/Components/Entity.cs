using System;

using DavidFidge.MonoGame.Core.Graphics;
using DavidFidge.MonoGame.Core.Interfaces.Graphics;
using DavidFidge.MonoGame.Core.Messages;

namespace DavidFidge.MonoGame.Core.Components
{
    public abstract class Entity : BaseComponent, ITransformable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ITransform Transform { get; set; } = new EntityTransform();

        public void TransformChanged()
        {
            Mediator?.Publish(new EntityTransformChanged(this));
        }
    }
}
