using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.View.Interfaces
{
    public interface IUserInterface
    {
        void Initialize(ContentManager content);
        void Initialize(ContentManager content, string theme);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void DrawMainRenderTarget(SpriteBatch spriteBatch);
        void SetActive(GeonBit.UI.IUserInterface userInterface);
        bool IsActive(GeonBit.UI.IUserInterface userInterface);
    }
}