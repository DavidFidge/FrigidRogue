using DavidFidge.MonoGame.Core.ConsoleCommands;

namespace DavidFidge.MonoGame.Core.Interfaces.ConsoleCommands
{
    public interface IConsoleCommandServiceFactory
    {
        IConsoleCommand CommandFor(ConsoleCommand command);
    }
}