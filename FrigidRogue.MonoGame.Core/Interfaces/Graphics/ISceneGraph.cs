using System;
using System.Collections.Generic;

using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Messages;
using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Interfaces.Graphics
{
    public interface ISceneGraph
    {
        void Initialise(Entity root);
        void LoadContent();
        void Draw(Matrix view, Matrix projection);
        void DeselectAll();
        void Add(Entity entity, Entity parent);
        void Remove(Entity entity);
        void ClearChildren(Entity entity);
        Entity Find(Predicate<Entity> predicate);
        Entity Select(Ray ray);
        List<Entity> GetEntitiesByBreadthFirstSearch();
        Matrix RecalculateWorldTransform(Entity entity);
        Matrix GetWorldTransformWithLocalTransform(Entity entity);
        Matrix GetWorldTransform(Entity entity);
        void HandleEntityTransformChanged(EntityTransformChangedNotification entityTransformChangedNotification);
    }
}