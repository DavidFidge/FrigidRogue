namespace FrigidRogue.MonoGame.Core.Interfaces.Components
{
    public interface IRandom
    {
        double NextDouble();
        int Next();
        int Next(int min, int max);
    }
}