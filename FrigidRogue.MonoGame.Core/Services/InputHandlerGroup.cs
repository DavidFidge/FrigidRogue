using System.Collections.Generic;

using InputHandlers.Keyboard;
using InputHandlers.Mouse;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class InputHandlerGroup
    {
        private readonly List<IKeyboardHandler> _keyboardHandlers = new List<IKeyboardHandler>();
        private readonly List<IMouseHandler> _mouseHandlers = new List<IMouseHandler>();

        public InputHandlerGroup(IMouseHandler mouseHandler, IKeyboardHandler keyboardHandler)
        {
            AddToGroup(mouseHandler, keyboardHandler);
        }

        public void AddToGroup(IMouseHandler mouseHandler, IKeyboardHandler keyboardHandler)
        {
            _mouseHandlers.Add(mouseHandler);
            _keyboardHandlers.Add(keyboardHandler);
        }

        public void RemoveFromGroup(IMouseHandler mouseHandler, IKeyboardHandler keyboardHandler)
        {
            _mouseHandlers.Remove(mouseHandler);
            _keyboardHandlers.Remove(keyboardHandler);
        }

        public void UnsubscribeGroup(IMouseInput mouseInput, IKeyboardInput keyboardInput)
        {
            foreach (var keyboardHandler in _keyboardHandlers)
            {
                keyboardInput.Unsubscribe(keyboardHandler);
            }

            foreach (var mouseHandler in _mouseHandlers)
            {
                mouseInput.Unsubscribe(mouseHandler);
            }
        }

        public void SubscribeGroup(IMouseInput mouseInput, IKeyboardInput keyboardInput)
        {
            foreach (var keyboardHandler in _keyboardHandlers)
            {
                keyboardInput.Subscribe(keyboardHandler);
            }

            foreach (var mouseHandler in _mouseHandlers)
            {
                mouseInput.Subscribe(mouseHandler);
            }
        }

        public bool ContainsHandlers(IKeyboardHandler keyboardHandler, IMouseHandler mouseHandler)
        {
            return _keyboardHandlers.Contains(keyboardHandler) && _mouseHandlers.Contains(mouseHandler);
        }
    }
}