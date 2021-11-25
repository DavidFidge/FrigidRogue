using FrigidRogue.MonoGame.Core.UserInterface;

using InputHandlers.Keyboard;

using Microsoft.Xna.Framework.Input;

namespace FrigidRogue.MonoGame.Core.Interfaces.UserInterface
{
    public interface IActionMap
    {
        bool ActionIs<T>(Keys key, KeyboardModifier keyboardModifier, string selector = null);

        bool ActionIs<T>(KeyCombination keyCombination, string selector = null);
    }
}