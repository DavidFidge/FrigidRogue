using FrigidRogue.MonoGame.Core.Services;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace FrigidRogue.MonoGame.Core.View.Interfaces
{
    public interface IUserInterface
    {
        RenderResolution RenderResolution { get; set; }
        void Initialize(ContentManager content);
        void Initialize(ContentManager content, string theme);
        void Update(GameTime gameTime);
        void DrawActiveScreen();
        void SetActive(Screen screen);
        void ShowScreen(IScreen screen);
        bool IsActive(GeonBit.UI.UserInterface userInterface);
        GeonBit.UI.UserInterface Create();
    }
}