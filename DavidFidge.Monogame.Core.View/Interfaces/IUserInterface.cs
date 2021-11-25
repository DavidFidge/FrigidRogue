using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DavidFidge.Monogame.Core.View.Interfaces
{
    public interface IUserInterface
    {
        void Initialize(ContentManager content);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void DrawMainRenderTarget(SpriteBatch spriteBatch);
    }
}