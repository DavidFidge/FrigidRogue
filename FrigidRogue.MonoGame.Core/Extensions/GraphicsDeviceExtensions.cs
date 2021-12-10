using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class GraphicsDeviceExtensions
    {
        public static Rectangle ViewportRectangle(this GraphicsDevice graphicsDevice)
        {
            return new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        }
    }
}
