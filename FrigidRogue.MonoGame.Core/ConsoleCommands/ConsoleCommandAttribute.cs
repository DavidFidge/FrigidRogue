namespace FrigidRogue.MonoGame.Core.ConsoleCommands
{
    public class ConsoleCommandAttribute : Attribute
    {
        public string Name { get; set; }
        public string Parameter1 { get; set; }
    }
}
