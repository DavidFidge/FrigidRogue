using System.Collections.Generic;

using FrigidRogue.MonoGame.Core.Interfaces.UserInterface;

namespace FrigidRogue.MonoGame.Core.UserInterface
{
    public class EmptyActionMapStore : IActionMapStore
    {
        public Dictionary<string, KeyCombination> GetKeyMap()
        {
            return new Dictionary<string, KeyCombination>();
        }
    }
}
