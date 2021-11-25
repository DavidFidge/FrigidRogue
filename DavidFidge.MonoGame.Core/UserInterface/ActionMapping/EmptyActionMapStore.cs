using System.Collections.Generic;

using DavidFidge.MonoGame.Core.Interfaces.UserInterface;

namespace DavidFidge.MonoGame.Core.UserInterface
{
    public class EmptyActionMapStore : IActionMapStore
    {
        public Dictionary<string, KeyCombination> GetKeyMap()
        {
            return new Dictionary<string, KeyCombination>();
        }
    }
}
