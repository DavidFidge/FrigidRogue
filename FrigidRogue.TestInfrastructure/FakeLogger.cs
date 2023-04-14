using Microsoft.VisualStudio.TestTools.UnitTesting;
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

    public void AssertLogEvent(string message, Exception exception)
    {
        var logEvent = LogEvents.FirstOrDefault(x => x.MessageTemplate.Text == message && x.Exception == exception);

        Assert.IsNotNull(logEvent, $"LogEvent not found for message: {message}, exception: {exception}");
    }

    public void AssertLogEvent(string message, Exception exception, LogEventLevel logEventLevel)
    {
        var logEvent = LogEvents.FirstOrDefault(x => x.Level == logEventLevel && x.MessageTemplate.Text == message && x.Exception == exception);

        Assert.IsNotNull(logEvent, $"LogEvent not found for logEventLevel: {logEventLevel}, message: {message}, exception: {exception}");
    }

    public void AssertLogEvent(string message, Exception exception, string exceptionMessage)
    {
        var logEvent = LogEvents.FirstOrDefault(x => x.MessageTemplate.Text == message && x.Exception == exception && x.Exception.Message == exceptionMessage);

        Assert.IsNotNull(logEvent, $"LogEvent not found for message: {message}, exception: {exception}, exceptionMessage: {exceptionMessage}");
    }

    public void AssertLogEvent(string message, Exception exception, string exceptionMessage, LogEventLevel logEventLevel)
    {
        var logEvent = LogEvents.FirstOrDefault(x => x.Level == logEventLevel && x.MessageTemplate.Text == message && x.Exception == exception && x.Exception.Message == exceptionMessage);

        Assert.IsNotNull(logEvent, $"LogEvent not found for logEventLevel: {logEventLevel}, message: {message}, exception: {exception}, exceptionMessage: {exceptionMessage}");
    }

    public void AssertLogEvent(string message)
    {
        var logEvent = LogEvents.FirstOrDefault(x => x.MessageTemplate.Text == message);

        Assert.IsNotNull(logEvent, $"LogEvent not found for message: {message}");
    }

    public void AssertLogEvent(string message, LogEventLevel logEventLevel)
    {
        var logEvent = LogEvents.FirstOrDefault(x => x.Level == logEventLevel && x.MessageTemplate.Text == message);

        Assert.IsNotNull(logEvent, $"LogEvent not found for logEventLevel: {logEventLevel}, message: {message}");
    }
}