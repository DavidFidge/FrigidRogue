using System;
using System.Collections.Generic;

namespace FrigidRogue.MonoGame.Core.Components
{
    public class CommandResult
    {
        public CommandResultEnum Result { get; set; }
        public IList<string> Messages { get; set; } = Array.Empty<string>();
        public IList<BaseCommand> SubsequentCommands { get; set; } = Array.Empty<BaseCommand>();

        public static CommandResult Failure(string message)
        {
            return Failure(new List<string> { message });
        }

        public static CommandResult Failure(List<string> messages)
        {
            return new CommandResult
            {
                Result = CommandResultEnum.Failure,
                Messages = messages
            };
        }

        public static CommandResult Success()
        {
            return new CommandResult
            {
                Result = CommandResultEnum.Success
            };
        }

        public static CommandResult Success(BaseCommand subsequentCommand)
        {
            return new CommandResult
            {
                Result = CommandResultEnum.Success,
                SubsequentCommands = new List<BaseCommand> { subsequentCommand }
            };
        }

        public static CommandResult Success(string message)
        {
            return Success(new List<string> { message });
        }

        public static CommandResult Success(List<string> messages)
        {
            return new CommandResult
            {
                Result = CommandResultEnum.Success,
                Messages = messages
            };
        }
    }
}