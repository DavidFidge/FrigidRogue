using FrigidRogue.MonoGame.Core.ConsoleCommands;

namespace FrigidRogue.MonoGame.Core.Interfaces.ConsoleCommands
{
    public interface IConsoleCommand
    {
        void Execute(ConsoleCommand consoleCommand);
    }
}