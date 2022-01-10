using InputHandlers.Keyboard;
using InputHandlers.Mouse;

using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Interfaces.Services
{
    public interface IGameInputService
    {
        void Poll(Rectangle viewportBounds);
        void Reset();
        void ChangeInput(IMouseHandler mouseHandler, IKeyboardHandler keyboardHandler);
        void AddToCurrentGroup(IMouseHandler mouseHandler, IKeyboardHandler keyboardHandler);
        void RevertInputUpToAndIncluding(IMouseHandler mouseHandler, IKeyboardHandler keyboardHandler);
        void RemoveFromCurrentGroup(IMouseHandler mouseHandler, IKeyboardHandler keyboardHandler);
    }
}