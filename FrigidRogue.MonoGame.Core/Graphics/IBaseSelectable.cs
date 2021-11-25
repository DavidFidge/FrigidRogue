using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Graphics
{
    public interface IBaseSelectable
    {
        float? Intersects(Ray ray, Matrix worldTransform);
    }
}