using FrigidRogue.MonoGame.Core.ConsoleCommands;

namespace FrigidRogue.MonoGame.Core.Interfaces.ConsoleCommands
{
    public interface IConsoleCommandServiceFactory
    {
        IConsoleCommand CommandFor(ConsoleCommand command);
    }
}