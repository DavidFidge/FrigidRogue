using DavidFidge.MonoGame.Core.UserInterface;

using MediatR;

using Microsoft.Xna.Framework.Input;

namespace DavidFidge.MonoGame.Core.Messages
{
    [ActionMap(Name = "Exit Game", DefaultKey = Keys.Escape)]
    public class ExitGameRequest : IRequest
    {
    }
}