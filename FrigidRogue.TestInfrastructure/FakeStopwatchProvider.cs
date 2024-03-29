using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.TestInfrastructure
{
    public class FakeStopwatchProvider : IStopwatchProvider
    {
        public TimeSpan Elapsed { get; set; }

        public FakeStopwatchProvider()
        {
            Reset();
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }

        public void Reset()
        {
            Elapsed = TimeSpan.Zero;
        }

        public void Restart()
        {
            Reset();
        }
    }
}
