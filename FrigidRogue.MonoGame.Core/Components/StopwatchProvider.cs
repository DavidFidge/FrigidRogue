using System.Diagnostics;
using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Components
{
    public class StopwatchProvider : IStopwatchProvider
    {
        private readonly Stopwatch _stopwatch;

        public StopwatchProvider()
        {
            _stopwatch = new Stopwatch();
        }

        public void Start()
        {
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }

        public void Reset()
        {
            _stopwatch.Reset();
        }

        public void Restart()
        {
            _stopwatch.Restart();
        }

        public TimeSpan Elapsed
        {
            get { return _stopwatch.Elapsed; }
        }
    }
}