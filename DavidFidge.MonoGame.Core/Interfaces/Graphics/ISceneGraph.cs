using System.Collections.Generic;

using DavidFidge.MonoGame.Core.Components;

using Microsoft.Xna.Framework;

namespace DavidFidge.MonoGame.Core.Interfaces.Graphics
{
    public interface ISceneGraph
    {
        void Initialise(Entity root);
        void LoadContent();
        void Draw(Matrix view, Matrix projection);
        void DeselectAll();
        void Add(Entity entity, Entity parent);
        void Remove(Entity entity);
        Entity Select(Ray ray);
        List<Entity> GetEntitiesByBreadthFirstSearch();
        Matrix RecalculateWorldTransform(Entity entity);
        Matrix GetWorldTransformWithLocalTransform(Entity entity);
        Matrix GetWorldTransform(Entity entity);
    }
}