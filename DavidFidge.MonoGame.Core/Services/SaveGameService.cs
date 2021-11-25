using System.Collections.Generic;

using DavidFidge.MonoGame.Core.Interfaces.Components;
using DavidFidge.MonoGame.Core.Interfaces.Services;

namespace DavidFidge.MonoGame.Core.Services
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
                saveableComponent.LoadGame(saveGameStore);
            }
        }

        public void SaveGame(ISaveGameStore saveGameStore)
        {
            foreach (var saveableComponent in _saveableComponent)
            {
                saveableComponent.SaveGame(saveGameStore);
            }
        }
    }
}