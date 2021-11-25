using System.Linq;

namespace FrigidRogue.MonoGame.Core.ConsoleCommands
{
    public class ConsoleCommand
    {
        private readonly string _command;

        public ConsoleCommand(string command)
        {
            _command = command;
        }

        public string Name => _command.Split(' ').First();

        public string Result { get; set; }

        public override string ToString()
        {
            return _command;
        }
    }
}