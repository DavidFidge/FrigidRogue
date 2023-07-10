using SadRogue.Primitives;

namespace FrigidRogue.MonoGame.Core.Components.MapPointChoiceRules;

public abstract class MapPointChoiceRule
{
    public abstract bool IsValid(Point point);
}
