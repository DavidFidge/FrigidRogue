using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Interfaces.UserInterface;

using InputHandlers.Keyboard;

using Microsoft.Xna.Framework.Input;

namespace FrigidRogue.MonoGame.Core.UserInterface
{
    public abstract class BaseKeyboardHandler : BaseComponent, IKeyboardHandler
    {
        public IActionMap ActionMap { get; set; }

        public virtual void HandleKeyboardKeyDown(Keys[] keysDown, Keys keyInFocus, KeyboardModifier keyboardModifier)
        {
        }

        public virtual void HandleKeyboardKeyLost(Keys[] keysDown, KeyboardModifier keyboardModifier)
        {
        }

        public virtual void HandleKeyboardKeyRepeat(Keys repeatingKey, KeyboardModifier keyboardModifier)
        {
        }

        public virtual void HandleKeyboardKeysReleased()
        {
        }
    }
}