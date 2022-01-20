using FrigidRogue.MonoGame.Core.ConsoleCommands;
using FrigidRogue.MonoGame.Core.Interfaces.ConsoleCommands;

namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseConsoleCommand : BaseComponent, IConsoleCommand
    {
        public abstract void Execute(ConsoleCommand consoleCommand);
    }
}