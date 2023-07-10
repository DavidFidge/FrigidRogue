using SadRogue.Primitives;

namespace FrigidRogue.MonoGame.Core.Components.MapPointChoiceRules;

public class RestrictedSetRule : MapPointChoiceRule
{
    private readonly List<Point> _set;

    public RestrictedSetRule(List<Point> set)
    {
        _set = set;
    }
    
    public override bool IsValid(Point point)
    {
        return _set.Contains(point);
    }
}