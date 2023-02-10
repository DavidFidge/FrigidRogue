using Serilog;
using Serilog.Events;

namespace FrigidRogue.TestInfrastructure;

public class FakeLogger : ILogger
{
    public List<LogEvent> LogEvents { get; } = new();
    
    public void Write(LogEvent logEvent)
    {
        LogEvents.Add(logEvent);
    }
}