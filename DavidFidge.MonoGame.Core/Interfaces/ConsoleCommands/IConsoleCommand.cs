using DavidFidge.MonoGame.Core.ConsoleCommands;

namespace DavidFidge.MonoGame.Core.Interfaces.ConsoleCommands
{
    public interface IConsoleCommand
    {
        void Execute(ConsoleCommand consoleCommand);
    }
}