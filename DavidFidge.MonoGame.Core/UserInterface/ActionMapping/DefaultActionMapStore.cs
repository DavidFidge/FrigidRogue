using System;
using System.Collections.Generic;
using System.Linq;

using Castle.Core.Internal;

using DavidFidge.MonoGame.Core.Interfaces.UserInterface;

namespace DavidFidge.MonoGame.Core.UserInterface
{
    public class DefaultActionMapStore : IActionMapStore
    {
        private readonly Dictionary<string, KeyCombination> _keyMap;

        public DefaultActionMapStore()
        {
            _keyMap = AppDomain.CurrentDomain
                .GetAssemblies()
                .AsParallel()
                .SelectMany(a => a.GetTypes())
                .SelectMany(t => t.GetAttributes<ActionMapAttribute>())
                .Where(t => t != null)
                .ToDictionary(t => t.Name, t => new KeyCombination(t.DefaultKey, t.DefaultKeyboardModifier));
            }

        public Dictionary<string, KeyCombination> GetKeyMap()
        {
            return _keyMap;
        }
    }
}