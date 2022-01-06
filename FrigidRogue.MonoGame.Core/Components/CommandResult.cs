using System;
using System.Collections.Generic;

namespace FrigidRogue.MonoGame.Core.Components
{
    public class CommandResult
    {
        public CommandResultEnum Result { get; set; }
        public IList<string> Messages { get; set; } = Array.Empty<string>();
        public IList<BaseGameActionCommand> SubsequentCommands { get; set; } = Array.Empty<BaseGameActionCommand>();

        public static CommandResult Failure(string message)
        {
            return Failure(new List<string> { message });
        }

        public static CommandResult NoMove(string message)
        {
            return NoMove(new List<string> { message });
        }

        public static CommandResult NoMove(List<string> messages)
        {
            return new CommandResult
            {
                Result = CommandResultEnum.NoMove,
                Messages = messages
            };
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

        public static CommandResult Success(BaseGameActionCommand subsequentCommand)
        {
            return new CommandResult
            {
                Result = CommandResultEnum.Success,
                SubsequentCommands = new List<BaseGameActionCommand> { subsequentCommand }
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