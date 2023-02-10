using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Components;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}