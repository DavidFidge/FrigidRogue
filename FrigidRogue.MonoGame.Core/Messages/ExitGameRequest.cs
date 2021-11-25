using FrigidRogue.MonoGame.Core.UserInterface;

using MediatR;

using Microsoft.Xna.Framework.Input;

namespace FrigidRogue.MonoGame.Core.Messages
{
    [ActionMap(Name = "Exit Game", DefaultKey = Keys.Escape)]
    public class ExitGameRequest : IRequest
    {
    }
}