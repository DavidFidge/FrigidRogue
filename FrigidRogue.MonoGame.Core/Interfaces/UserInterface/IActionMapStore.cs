using System.Collections.Generic;

using FrigidRogue.MonoGame.Core.UserInterface;

namespace FrigidRogue.MonoGame.Core.Interfaces.UserInterface
{
    public interface IActionMapStore
    {
        Dictionary<string, KeyCombination> GetKeyMap();
    }
}