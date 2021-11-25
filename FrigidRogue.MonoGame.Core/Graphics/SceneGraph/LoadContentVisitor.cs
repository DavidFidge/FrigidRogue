using FrigidRogue.MonoGame.Core.Components;

using NGenerics.Patterns.Visitor;

namespace FrigidRogue.MonoGame.Core.Graphics
{
    public class LoadContentVisitor : IVisitor<Entity>
    {
        public void Visit(Entity entity)
        {
            if (entity is ILoadContent loadContent)
            {
                loadContent.LoadContent();
            }
        }

        public bool HasCompleted => false;
    }
}