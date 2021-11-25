using System;
using System.Linq;

using Castle.Core.Internal;

using FrigidRogue.MonoGame.Core.Interfaces.UserInterface;

using InputHandlers.Keyboard;

using Microsoft.Xna.Framework.Input;

namespace FrigidRogue.MonoGame.Core.UserInterface
{
    public class ActionMap : IActionMap
    {
        private readonly IActionMapStore _actionMapStore;

        public ActionMap(IActionMapStore actionMapStore)
        {
            _actionMapStore = actionMapStore;
        }

        public bool ActionIs<T>(Keys key, KeyboardModifier keyboardModifier, string selector = null)
        {
            return ActionIs<T>(new KeyCombination(key, keyboardModifier), selector);
        }

        public bool ActionIs<T>(KeyCombination keyCombination, string selector = null)
        {
            var actionMaps = typeof(T).GetAttributes<ActionMapAttribute>().ToList();

            if (actionMaps.IsNullOrEmpty())
                throw new Exception($"No {typeof(ActionMapAttribute).Name} found on class {typeof(T).Name}");

            var actionMap = actionMaps.First();

            if (selector != null)
            {
                actionMap = actionMaps.SingleOrDefault(a => a.Name == selector);

                if (actionMap == null)
                    throw new Exception($"No {typeof(ActionMapAttribute).Name} with name {selector} found on class {typeof(T).Name}");
            }

            var actionToKey = _actionMapStore.GetKeyMap();

            if (!actionToKey.ContainsKey(actionMap.Name))
                return false;

            return keyCombination.Equals(actionToKey[actionMap.Name]);
        }
    }
}
