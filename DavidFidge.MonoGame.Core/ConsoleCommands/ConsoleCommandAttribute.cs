using System;

namespace DavidFidge.MonoGame.Core.ConsoleCommands
{
    public class ConsoleCommandAttribute : Attribute
    {
        public string Name { get; set; }
    }
}