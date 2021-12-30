using System.Collections.Generic;

using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class SaveGameService : ISaveGameService
    {
        private List<ISaveable> _saveableComponent = new List<ISaveable>();

        public void Register(ISaveable saveableComponent)
        {
            _saveableComponent.Add(saveableComponent);
        }

        public void LoadGame(ISaveGameStore saveGameStore)
        {
            foreach (var saveableComponent in _saveableComponent)
            {
                saveableComponent.LoadState(saveGameStore);
            }
        }

        public void SaveGame(ISaveGameStore saveGameStore)
        {
            foreach (var saveableComponent in _saveableComponent)
            {
                saveableComponent.SaveState(saveGameStore);
            }
        }
    }
}