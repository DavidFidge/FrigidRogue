using Microsoft.Xna.Framework;

namespace DavidFidge.MonoGame.Core.Graphics
{
    public interface IBaseSelectable
    {
        float? Intersects(Ray ray, Matrix worldTransform);
    }
}