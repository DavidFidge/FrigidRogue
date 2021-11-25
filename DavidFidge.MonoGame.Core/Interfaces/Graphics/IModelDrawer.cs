using Microsoft.Xna.Framework;

using IDrawable = DavidFidge.MonoGame.Core.Graphics.IDrawable;

namespace DavidFidge.MonoGame.Core.Interfaces.Graphics
{
    public interface IModelDrawer : IDrawable
    {
        void ChangeTransform(Matrix[] transforms);
    }
}