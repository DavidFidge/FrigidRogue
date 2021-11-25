using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Graphics;

using Microsoft.Xna.Framework;

using NGenerics.Patterns.Visitor;

namespace FrigidRogue.MonoGame.Core.Graphics
{
    public class DrawVisitor : IVisitor<Entity>
    {
        private readonly Matrix _view;
        private readonly Matrix _projection;
        private readonly ISceneGraph _sceneGraph;

        public DrawVisitor(Matrix view, Matrix projection, ISceneGraph sceneGraph)
        {
            _view = view;
            _projection = projection;
            _sceneGraph = sceneGraph;
        }

        public void Visit(Entity entity)
        {
            if (entity is IDrawable drawable)
            {
                drawable.Draw(_view, _projection, _sceneGraph.GetWorldTransform(entity));
            }
        }

        public bool HasCompleted => false;
    }
}