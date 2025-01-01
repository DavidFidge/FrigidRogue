using Castle.Core.Internal;
using FrigidRogue.MonoGame.Core.Interfaces.UserInterface;

namespace FrigidRogue.MonoGame.Core.UserInterface
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
