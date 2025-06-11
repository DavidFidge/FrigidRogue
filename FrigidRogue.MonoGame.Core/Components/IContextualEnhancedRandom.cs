using ShaiRandom.Generators;

namespace FrigidRogue.MonoGame.Core.Components
{
    public interface IContextualEnhancedRandom : IEnhancedRandom
    {
        IEnhancedRandom EnhancedRandom { get; set; }

        bool NextBool(string context);
    }
}