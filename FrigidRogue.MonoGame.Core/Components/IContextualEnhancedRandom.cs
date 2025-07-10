using FrigidRogue.MonoGame.Core.Interfaces.Components;

using ShaiRandom.Generators;

namespace FrigidRogue.MonoGame.Core.Components
{
    public interface IContextualEnhancedRandom : IEnhancedRandom
    {
        MizuchiRandom GetSaveState();
        bool NextBool(string context);
        void Reset(ulong seed);
        void SetLoadState(MizuchiRandom loadState);
    }
}