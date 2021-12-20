using System;
using System.Collections.Generic;
using System.Linq;

using FrigidRogue.MonoGame.Core.Interfaces.Services;

using InputHandlers.Keyboard;
using InputHandlers.Mouse;

using Microsoft.Xna.Framework.Input;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class GameInputService : IGameInputService
    {
        private readonly IMouseInput _mouseInput;
        private readonly IKeyboardInput _keyboardInput;

        private readonly Stack<InputHandlerGroup> _inputHandlerGroups = new Stack<InputHandlerGroup>();

        public GameInputService(IMouseInput mouseInput, IKeyboardInput keyboardInput)
        {
            _mouseInput = mouseInput;
            _keyboardInput = keyboardInput;
            _keyboardInput.RepeatDelay = 500;
        }

        public void Poll()
        {
            _keyboardInput.Poll(Keyboard.GetState());
            _mouseInput.Poll(Mouse.GetState());
        }

        public void Reset()
        {
            _keyboardInput.Reset();
            _mouseInput.Reset();
        }

        public void ChangeInput(IMouseHandler mouseHandler, IKeyboardHandler keyboardHandler)
        {
            if (keyboardHandler == null)
                throw new ArgumentNullException(nameof(keyboardHandler));

            if (mouseHandler == null)
                throw new ArgumentNullException(nameof(mouseHandler));

            if (_inputHandlerGroups.Any())
            {
                _inputHandlerGroups
                    .Peek()
                    .UnsubscribeGroup(_mouseInput, _keyboardInput);
            }

            var inputHandlerGroup = new InputHandlerGroup(mouseHandler, keyboardHandler);
            _inputHandlerGroups.Push(inputHandlerGroup);

            inputHandlerGroup.SubscribeGroup(_mouseInput, _keyboardInput);
        }

        public void AddToCurrentGroup(IMouseHandler mouseHandler, IKeyboardHandler keyboardHandler)
        {
            if (keyboardHandler == null)
                throw new ArgumentNullException(nameof(keyboardHandler));

            if (mouseHandler == null)
                throw new ArgumentNullException(nameof(mouseHandler));

            if (_inputHandlerGroups.Any())
            {
                _mouseInput.Subscribe(mouseHandler);
                _keyboardInput.Subscribe(keyboardHandler);

                _inputHandlerGroups
                    .Peek()
                    .AddToGroup(mouseHandler, keyboardHandler);
            }
        }

        public void RemoveFromCurrentGroup(IMouseHandler mouseHandler, IKeyboardHandler keyboardHandler)
        {
            if (keyboardHandler == null)
                throw new ArgumentNullException(nameof(keyboardHandler));

            if (mouseHandler == null)
                throw new ArgumentNullException(nameof(mouseHandler));

            if (_inputHandlerGroups.Any())
            {
                _mouseInput.Unsubscribe(mouseHandler);
                _keyboardInput.Unsubscribe(keyboardHandler);

                _inputHandlerGroups
                    .Peek()
                    .RemoveFromGroup(mouseHandler, keyboardHandler);
            }
        }

        public void RevertInputUpToAndIncluding(IMouseHandler mouseHandler, IKeyboardHandler keyboardHandler)
        {
            if (keyboardHandler == null)
                throw new ArgumentNullException(nameof(keyboardHandler));

            if (mouseHandler == null)
                throw new ArgumentNullException(nameof(mouseHandler));

            while (_inputHandlerGroups.Any())
            {
                var inputHandlerGroup = _inputHandlerGroups.Pop();

                inputHandlerGroup.UnsubscribeGroup(_mouseInput, _keyboardInput);

                if (inputHandlerGroup.ContainsHandlers(keyboardHandler, mouseHandler))
                    break;
            }

            if (_inputHandlerGroups.Any())
            {
                var inputHandlerGroup = _inputHandlerGroups.Peek();
                inputHandlerGroup.SubscribeGroup(_mouseInput, _keyboardInput);
            }
        }
    }
}