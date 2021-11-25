using GeonBit.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using GeonBitUserInterface = GeonBit.UI.UserInterface;
using IGeonBitUserInterface = GeonBit.UI.IUserInterface;
using IUserInterface = FrigidRogue.MonoGame.Core.View.Interfaces.IUserInterface;

namespace FrigidRogue.MonoGame.Core.View
{
    // This is a wrapper around GeonBit's UserInterface class which contains a mix of statics (singleton that captures global state)
    // and instances of user interfaces that are effectively different screens in the game.
    public class UserInterface : IUserInterface
    {
        public void Initialize(ContentManager content)
        {
            // create and init the UI manager
            GeonBitUserInterface.Initialize(content, BuiltinThemes.hd);
            IntialiseActiveUserInterface();
        }

        private void IntialiseActiveUserInterface()
        {
            if (GeonBitUserInterface.Active == null)
                return;

            GeonBitUserInterface.Active.UseRenderTarget = true;

            // draw cursor outside the render target
            GeonBitUserInterface.Active.IncludeCursorInRenderTarget = false;
        }

        public void Initialize(ContentManager content, string theme)
        {
            // create and init the UI manager
            GeonBitUserInterface.Initialize(content, theme);
            IntialiseActiveUserInterface();
        }

        public void Update(GameTime gameTime)
        {
            GeonBitUserInterface.Active.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GeonBitUserInterface.Active.Draw(spriteBatch);
        }

        public void DrawMainRenderTarget(SpriteBatch spriteBatch)
        {
            GeonBitUserInterface.Active.DrawMainRenderTarget(spriteBatch);
        }

        public void SetActive(IGeonBitUserInterface userInterface)
        {
            GeonBitUserInterface.Active = userInterface;
        }

        public bool IsActive(IGeonBitUserInterface userInterface)
        {
            return GeonBitUserInterface.Active == userInterface;
        }
    }
}