namespace FrigidRogue.MonoGame.Core.ConsoleCommands
{
    public class ConsoleCommand
    {
        private readonly string _command;

        public string Name { get; }
        public IList<string> Params { get; }
        public string Result { get; set; }

        public ConsoleCommand(string command)
        {
            _command = command;
            Name = _command.Split(' ').First();
            Params = _command.Split(' ').Skip(1).ToList();
        }

        public override string ToString()
        {
            return _command;
        }
    }
}
