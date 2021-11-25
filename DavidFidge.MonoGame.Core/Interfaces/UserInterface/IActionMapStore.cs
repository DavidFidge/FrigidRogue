using System.Collections.Generic;

using DavidFidge.MonoGame.Core.UserInterface;

namespace DavidFidge.MonoGame.Core.Interfaces.UserInterface
{
    public interface IActionMapStore
    {
        Dictionary<string, KeyCombination> GetKeyMap();
    }
}