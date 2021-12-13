using FrigidRogue.MonoGame.Core.UserInterface;

using MediatR;

using Microsoft.Xna.Framework.Input;

namespace FrigidRogue.MonoGame.Core.Messages
{
    [ActionMap(Name = "Quit to Desktop", DefaultKey = Keys.Escape)]
    public class QuitToDesktopRequest : IRequest
    {
    }
}