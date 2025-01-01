using InputHandlers.Keyboard;
using Microsoft.Xna.Framework.Input;

namespace FrigidRogue.MonoGame.Core.UserInterface
{
    public struct KeyCombination
    {
        public readonly Keys Key;
        public readonly KeyboardModifier KeyboardModifier;

        public KeyCombination(Keys key)
        {
            Key = key;
            KeyboardModifier = KeyboardModifier.None;
        }

        public KeyCombination(Keys key, KeyboardModifier keyboardModifier)
        {
            Key = key;
            KeyboardModifier = keyboardModifier;
        }

        public bool Equals(KeyCombination other)
        {
            return Key == other.Key && KeyboardModifier == other.KeyboardModifier;
        }

        public override bool Equals(object other)
        {
            if (!(other is KeyCombination))
                return false;

            return Equals((KeyCombination)other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Key * 397) ^ (int) KeyboardModifier;
            }
        }
    }
}
