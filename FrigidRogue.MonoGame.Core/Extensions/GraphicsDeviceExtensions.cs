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
        
        public static void RestoreGraphicsDeviceAfterSpriteBatchDraw(this GraphicsDevice graphicsDevice)
        {
            // Reset graphics device properties after SpriteBatch drawing
            // https://shawnhargreaves.com/blog/spritebatch-and-renderstates.html
            graphicsDevice.BlendState = BlendState.Opaque;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;
            graphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
        }
    }
}
