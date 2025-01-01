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

        public bool ActionIs<T>(Keys key, string selector = null)
        {
            return ActionIs<T>(new KeyCombination(key, KeyboardModifier.None), selector);
        }

        public bool ActionIs<T>(Keys key, KeyboardModifier keyboardModifier, string selector = null)
        {
            return ActionIs<T>(new KeyCombination(key, keyboardModifier), selector);
        }

        public string ActionName<T>(Keys key, KeyboardModifier keyboardModifier)
        {
            var keyCombination = new KeyCombination(key, keyboardModifier);

            var actionMaps = typeof(T).GetAttributes<ActionMapAttribute>().ToList();

            var actionMap = actionMaps.Single(am => KeyMatchesAction(keyCombination, am));

            if (actionMap == null)
                throw new Exception(
                    $"No {typeof(ActionMapAttribute).Name} with key {key} found on class {typeof(T).Name}"
                );

            return actionMap.Name;
        }

        public bool ActionIs<T>(KeyCombination keyCombination, string selector = null)
        {
            var actionMaps = typeof(T).GetAttributes<ActionMapAttribute>().ToList();

            if (actionMaps.IsNullOrEmpty())
                throw new Exception($"No {typeof(ActionMapAttribute).Name} found on class {typeof(T).Name}");

            if (selector != null)
            {
                var actionMap = actionMaps.SingleOrDefault(a => a.Name == selector);

                if (actionMap == null)
                    throw new Exception(
                        $"No {typeof(ActionMapAttribute).Name} with name {selector} found on class {typeof(T).Name}"
                    );

                return KeyMatchesAction(keyCombination, actionMap);
            }

            return actionMaps.Any(actionMap => KeyMatchesAction(keyCombination, actionMap));
        }

        private bool KeyMatchesAction(KeyCombination keyCombination, ActionMapAttribute actionMap)
        {
            var actionToKey = _actionMapStore.GetKeyMap();

            if (!actionToKey.ContainsKey(actionMap.Name))
                return false;

            return keyCombination.Equals(actionToKey[actionMap.Name]);
        }
    }
}
