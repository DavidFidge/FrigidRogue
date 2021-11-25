using InputHandlers.Keyboard;
using InputHandlers.Mouse;

namespace DavidFidge.MonoGame.Core.Interfaces.Services
{
    public interface IGameInputService
    {
        void Poll();
        void Reset();
        void ChangeInput(IMouseHandler mouseHandler, IKeyboardHandler keyboardHandler);
        void AddToCurrentGroup(IMouseHandler mouseHandler, IKeyboardHandler keyboardHandler);
        void RevertInputUpToAndIncluding(IMouseHandler mouseHandler, IKeyboardHandler keyboardHandler);
        void RemoveFromCurrentGroup(IMouseHandler mouseHandler, IKeyboardHandler keyboardHandler);
    }
}