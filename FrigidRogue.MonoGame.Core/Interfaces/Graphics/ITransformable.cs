﻿namespace FrigidRogue.MonoGame.Core.Interfaces.Graphics
{
    public interface ITransformable
    {
        ITransform Transform { get; }
        void TransformChanged();
    }
}