using SadRogue.Primitives;

namespace FrigidRogue.MonoGame.Core.Components.MapPointChoiceRules;

public class MinDistanceRule : MapPointChoiceRule
{
    private readonly Point _source;
    private readonly uint _minDistance;

    public MinDistanceRule(Point source, uint minDistance)
    {
        _source = source;
        _minDistance = minDistance;
    }
    
    public override bool IsValid(Point point)
    {
        return Distance.Chebyshev.Calculate(_source, point) >= _minDistance;
    }
}