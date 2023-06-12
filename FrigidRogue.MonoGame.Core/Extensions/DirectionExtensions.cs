using SadRogue.Primitives;

namespace FrigidRogue.MonoGame.Core.Extensions;

public static class DirectionExtensions
{
    public static Direction Opposite(this Direction direction)
    {
        return direction.Type switch
        {
            Direction.Types.Up => Direction.Down,
            Direction.Types.Down => Direction.Up,
            Direction.Types.Left => Direction.Right,
            Direction.Types.Right => Direction.Left,
            Direction.Types.UpLeft => Direction.DownRight,
            Direction.Types.UpRight => Direction.DownLeft,
            Direction.Types.DownLeft => Direction.UpRight,
            Direction.Types.DownRight => Direction.UpLeft,
            _ => Direction.None
        };
    }
}