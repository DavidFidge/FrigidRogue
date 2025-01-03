using Castle.Core.Internal;
using FrigidRogue.MonoGame.Core.Interfaces.ConsoleCommands;

namespace FrigidRogue.MonoGame.Core.ConsoleCommands
{
    public class ConsoleCommandServiceFactory : IConsoleCommandServiceFactory
    {
        private readonly Dictionary<string, IConsoleCommand> _consoleCommands;

        public ConsoleCommandServiceFactory(IConsoleCommand[] consoleCommands)
        {
            _consoleCommands = consoleCommands
                .ToDictionary(c => c.GetType().GetAttributes<ConsoleCommandAttribute>().Single().Name.ToLower());
        }

        public IConsoleCommand CommandFor(ConsoleCommand command)
        {
            if (!_consoleCommands.ContainsKey(command.Name.ToLower()))
                return null;

            return _consoleCommands[command.Name.ToLower()];
        }
    }
}
