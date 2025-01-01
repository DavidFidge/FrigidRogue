using FrigidRogue.MonoGame.Core.Components.Mediator;
using FrigidRogue.MonoGame.Core.UserInterface;
using Microsoft.Xna.Framework.Input;

namespace FrigidRogue.MonoGame.Core.Messages
{
    [ActionMap(Name = "Quit to Desktop", DefaultKey = Keys.Escape)]
    public class QuitToDesktopRequest : IRequest
    {
    }
}