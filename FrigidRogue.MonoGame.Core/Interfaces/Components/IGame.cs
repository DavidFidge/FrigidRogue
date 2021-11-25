using System;

using FrigidRogue.MonoGame.Core.Graphics;
using FrigidRogue.MonoGame.Core.Messages;
using FrigidRogue.MonoGame.Core.Services;

using MediatR;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Interfaces.Components
{
    public interface IGame :
        IRequestHandler<ExitGameRequest>,
        IDisposable
    {
        GameComponentCollection Components { get; }
        GameServiceContainer Services { get; }
        ContentManager Content { get; set; }
        GraphicsDevice GraphicsDevice { get; }
        GameWindow Window { get; }
        CustomGraphicsDeviceManager CustomGraphicsDeviceManager { get; }
        void Run();
        EffectCollection EffectCollection { get; }
    }
}