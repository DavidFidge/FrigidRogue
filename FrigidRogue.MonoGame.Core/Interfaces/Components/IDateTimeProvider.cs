namespace FrigidRogue.MonoGame.Core.Interfaces.Components;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}