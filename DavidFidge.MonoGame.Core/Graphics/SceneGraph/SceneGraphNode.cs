﻿using DavidFidge.MonoGame.Core.Components;
using DavidFidge.MonoGame.Core.Interfaces.Graphics;

using Microsoft.Xna.Framework;

using NGenerics.DataStructures.Trees;

namespace DavidFidge.MonoGame.Core.Graphics
{
    public class SceneGraphNode
    {
        public GeneralTree<Entity> Node { get; set; }
        public Matrix? WorldTransform { get; set; }

        public SceneGraphNode(Entity entity)
        {
            Node = new GeneralTree<Entity>(entity);
            WorldTransform = null;
        }

        public void AddChild(SceneGraphNode child)
        {
            Node.Add(child.Node);
        }
    }
}