using System;
using System.Collections.Generic;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Services;
using FrigidRogue.MonoGame.Core.View.Interfaces;

using GeonBit.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.View
{
    // This is a wrapper around GeonBit's UserInterface class which contains a mix of statics (singleton that captures global state)
    // and instances of user interfaces that are effectively different screens in the game.
    public class GeonBitUserInterfaceWrapper : IUserInterface
    {
        private readonly IGameProvider _gameProvider;

        public GeonBitUserInterfaceWrapper(IGameProvider gameProvider)
        {
            _gameProvider = gameProvider;
        }

        public RenderResolution RenderResolution
        {
            get => _renderResolution;
            set
            {
                _renderResolution = value;

                foreach (var userInterface in _userInterfaces)
                {
                    userInterface.RenderTargetWidth = _renderResolution.Width;
                    userInterface.RenderTargetHeight = _renderResolution.Height;
                }
            }
        }

        private readonly List<GeonBit.UI.UserInterface> _userInterfaces = new List<GeonBit.UI.UserInterface>();

        private RenderResolution _renderResolution;

        private IScreen _activeScreen;

        public void Initialize(ContentManager content)
        {
            // create and init the UI manager
            GeonBit.UI.UserInterface.Initialize(content, BuiltinThemes.hd);
        }

        public void Initialize(ContentManager content, string theme)
        {
            // create and init the UI manager
            GeonBit.UI.UserInterface.Initialize(content, theme);
        }

        public GeonBit.UI.UserInterface Create()
        {
            var userInterface = new GeonBit.UI.UserInterface();
            _userInterfaces.Add(userInterface);

            userInterface.UseRenderTarget = true;
            userInterface.IncludeCursorInRenderTarget = false;

            if (RenderResolution != null)
            {
                userInterface.RenderTargetWidth = RenderResolution.Width;
                userInterface.RenderTargetHeight = RenderResolution.Height;
            }

            return userInterface;
        }

        public void Update(GameTime gameTime)
        {
            GeonBit.UI.UserInterface.Active.Update(gameTime);

            _activeScreen?.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GeonBit.UI.UserInterface.Active.Draw(spriteBatch);

            _gameProvider.Game.GraphicsDevice.Clear(Color.Black);

            // Reset graphics device properties after SpriteBatch drawing
            // https://blogs.msdn.microsoft.com/shawnhar/2010/06/18/spritebatch-and-renderstates-in-xna-game-studio-4-0/
            _gameProvider.Game.GraphicsDevice.BlendState = BlendState.Opaque;
            _gameProvider.Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            _gameProvider.Game.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            _activeScreen?.Draw();

            GeonBit.UI.UserInterface.Active.DrawMainRenderTarget(spriteBatch);
        }

        public void SetActive(Screen screen)
        {
            _activeScreen = screen;
            
            GeonBit.UI.UserInterface.Active = screen.ScreenUserInterface;
        }

        public bool IsActive(GeonBit.UI.UserInterface userInterface)
        {
            return GeonBit.UI.UserInterface.Active == userInterface;
        }

        public void ShowScreen(IScreen screen)
        {
            _activeScreen?.Hide();

            screen.Show();

            _activeScreen = screen;
        }
    }
}