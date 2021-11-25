using System;

namespace FrigidRogue.MonoGame.Core.Interfaces.Components
{
    public interface IStopwatchProvider
    {
        void Start();
        void Stop();
        void Reset();
        TimeSpan Elapsed { get; }
        void Restart();
    }
}