using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.MonoGame.Core.Interfaces.Graphics;
using FrigidRogue.MonoGame.Core.Messages;
using Microsoft.Xna.Framework;
using NGenerics.Patterns.Visitor;

namespace FrigidRogue.MonoGame.Core.Graphics
{
    public class SceneGraph : ISceneGraph
    {
        private SceneGraphNode Root { get; set; }
        private Dictionary<Entity, SceneGraphNode> _sceneGraphNodes = new Dictionary<Entity, SceneGraphNode>();

        public void Initialise(Entity root)
        {
            _sceneGraphNodes = new Dictionary<Entity, SceneGraphNode>();
            Root = new SceneGraphNode(root);
            _sceneGraphNodes[root] = Root;
        }

        public void LoadContent()
        {
            Root.Node.BreadthFirstTraversal(new LoadContentVisitor());
        }

        public void Draw(Matrix view, Matrix projection)
        {
            Root.Node.BreadthFirstTraversal(new DrawVisitor(view, projection, this));
        }

        public void DeselectAll()
        {
            Root.Node.BreadthFirstTraversal(new ActionVisitor<Entity>(
                e =>
                {
                    if (e is ISelectable deselect)
                        deselect.IsSelected = false;
                }));
        }

        public void Add(Entity entity, Entity parent)
        {
            var node = new SceneGraphNode(entity);

            _sceneGraphNodes[entity] = node;
            _sceneGraphNodes[parent].AddChild(node);
        }

        public Entity Find(Predicate<Entity> predicate)
        {
            return _sceneGraphNodes.Keys.FirstOrDefault(k => predicate(k));
        }

        public void ClearChildren(Entity entity)
        {
            var node = Root.Node.FindNode(n => n.Equals(entity));

            var visitor = new BreadthFirstNodeCollectionVisitor();
            node.BreadthFirstTraversal(visitor);

            foreach (var entityUnderRemovedNode in visitor.VisitedEntities)
            {
                if (entityUnderRemovedNode != node.Data)
                {
                    _sceneGraphNodes.Remove(entityUnderRemovedNode);
                    node.Remove(entityUnderRemovedNode);
                }
            }
        }

        public void Remove(Entity entity)
        {
            if (Root.Node.Data == entity)
                throw new Exception("Cannot remove root node");

            var node = Root.Node.FindNode(n => n == entity);

            var visitor = new BreadthFirstNodeCollectionVisitor();
            node.BreadthFirstTraversal(visitor);

            foreach (var entityUnderRemovedNode in visitor.VisitedEntities)
            {
                _sceneGraphNodes.Remove(entityUnderRemovedNode);
            }

            node.Parent.Remove(node);
        }


        public Entity Select(Ray ray)
        {
            var selectVisitor = new SelectVisitor(ray, this);
            
            Root.Node.BreadthFirstTraversal(selectVisitor);

            var selectedEntity = selectVisitor.SelectedEntities.OrderBy(e => e.Distance);

            if (selectedEntity.Any())
                return selectedEntity.First().Entity;

            return null;
        }

        public List<Entity> GetEntitiesByBreadthFirstSearch()
        {
            var breadthFirstNodeCollectionVisitor = new BreadthFirstNodeCollectionVisitor();

            Root.Node.BreadthFirstTraversal(breadthFirstNodeCollectionVisitor);

            return breadthFirstNodeCollectionVisitor.VisitedEntities;
        }

        public void RecalculateWorldTransforms(Entity entity = null)
        {
            var startingNode = Root;

            if (entity != null)
                startingNode = _sceneGraphNodes[entity];

            startingNode.Node.BreadthFirstTraversal(new ActionVisitor<Entity>(
                e =>
                {
                    var parentTransform = Matrix.Identity;

                    var parent = _sceneGraphNodes[e].Node.Parent;

                    if (parent != null)
                        parentTransform = _sceneGraphNodes[parent.Data].WorldTransform.Value;
                    
                    _sceneGraphNodes[e].WorldTransform = parentTransform * e.Transform.Transform;
                }));
        }

        public Matrix RecalculateWorldTransform(Entity entity)
        {
            var parent = _sceneGraphNodes[entity].Node.Parent;

            if (parent == null)
            {
                if (_sceneGraphNodes[entity].WorldTransform == null)
                    _sceneGraphNodes[entity].WorldTransform = entity.Transform.Transform;

                return _sceneGraphNodes[entity].WorldTransform.Value;
            }

            var worldTransform = entity.Transform.Transform * RecalculateWorldTransform(parent.Data).SetScaleOne();

            _sceneGraphNodes[entity].WorldTransform = worldTransform;

            return worldTransform;
        }

        public Matrix GetWorldTransformWithLocalTransform(Entity entity)
        {
            var worldTransform = Matrix.Identity;
            var parent = _sceneGraphNodes[entity].Node.Parent?.Data;

            if (parent != null)
                worldTransform = _sceneGraphNodes[parent].WorldTransform ?? worldTransform;

            // Only take scale from the entity's transform
            var transform = entity.Transform.Transform * worldTransform.SetScaleOne();

            return transform;
        }

        public Matrix GetWorldTransform(Entity entity)
        {
            if (_sceneGraphNodes[entity].WorldTransform == null)
                RecalculateWorldTransform(entity);

            return _sceneGraphNodes[entity].WorldTransform.Value;
        }

        public void HandleEntityTransformChanged(EntityTransformChangedNotification entityTransformChangedNotification)
        {
            InvalidateWorldTransform(entityTransformChangedNotification.Entity);
        }

        public void InvalidateWorldTransform(Entity entity)
        {
            _sceneGraphNodes.TryGetValue(entity, out SceneGraphNode sceneGraphNode);

            if (sceneGraphNode?.WorldTransform == null)
                return;

            _sceneGraphNodes[entity].Node.BreadthFirstTraversal(new ActionVisitor<Entity>(e => _sceneGraphNodes[e].WorldTransform = null));
        }
    }

    public class BreadthFirstNodeCollectionVisitor: IVisitor<Entity>
    {
        public List<Entity> VisitedEntities { get; set; } = new List<Entity>();

        public void Visit(Entity entity)
        {
            VisitedEntities.Add(entity);
        }

        public bool HasCompleted => false;
    }

    public struct SelectedEntity
    {
        public Entity Entity;
        public float Distance;
    }
}
