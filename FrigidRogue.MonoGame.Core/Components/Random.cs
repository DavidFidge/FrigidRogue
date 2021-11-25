using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Components
{
    public class Random : IRandom
    {
        private System.Random _random;

        public Random()
        {
            _random = new System.Random();
        }

        public double NextDouble()
        {
            return _random.NextDouble();
        }

        public int Next()
        {
            return _random.Next();
        }

        public int Next(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}