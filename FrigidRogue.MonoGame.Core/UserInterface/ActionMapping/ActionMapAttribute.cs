using InputHandlers.Keyboard;
using Microsoft.Xna.Framework.Input;

namespace FrigidRogue.MonoGame.Core.UserInterface
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ActionMapAttribute : Attribute
    {
        public string Name { get; set; }
        public Keys DefaultKey { get; set; }
        public KeyboardModifier DefaultKeyboardModifier { get; set; }

        public ActionMapAttribute()
        {
            DefaultKeyboardModifier = KeyboardModifier.None;
        }
    }
}
