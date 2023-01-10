using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Graphics;

using Microsoft.Xna.Framework;

using NGenerics.Patterns.Visitor;

namespace FrigidRogue.MonoGame.Core.Graphics
{
    public class SelectVisitor : IVisitor<Entity>
    {
        private readonly Ray _ray;
        private readonly ISceneGraph _sceneGraph;

        public List<SelectedEntity> SelectedEntities { get; set; } = new List<SelectedEntity>();

        public SelectVisitor(Ray ray, ISceneGraph sceneGraph)
        {
            _ray = ray;
            _sceneGraph = sceneGraph;
        }

        public void Visit(Entity entity)
        {
            if (entity is ISelectable selectable)
            {
                var intersectDistance = selectable.Intersects(_ray, _sceneGraph.GetWorldTransform(entity));

                if (intersectDistance != null)
                    SelectedEntities.Add(new SelectedEntity { Entity = entity, Distance = intersectDistance.Value });
            }
        }

        public bool HasCompleted { get; } = false;
    }
}
