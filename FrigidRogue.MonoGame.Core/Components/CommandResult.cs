namespace FrigidRogue.MonoGame.Core.Components
{
    public class CommandResult
    {
        public CommandResultEnum Result { get; set; }
        public IList<string> Messages { get; set; } = Array.Empty<string>();
        public BaseGameActionCommand Command { get; set; }
        public IList<BaseGameActionCommand> SubsequentCommands { get; set; } = new List<BaseGameActionCommand>();

        private CommandResult()
        {
        }

        public static CommandResult Exception(BaseGameActionCommand command, string message)
        {
            return Exception(command, new List<string> { message });
        }

        public static CommandResult NoMove(BaseGameActionCommand command, string message)
        {
            return NoMove(command, new List<string> { message });
        }

        public static CommandResult NoMove(BaseGameActionCommand command, List<string> messages)
        {
            return new CommandResult
            {
                Command = command,
                Result = CommandResultEnum.NoMove,
                Messages = messages
            };
        }

        public static CommandResult Exception(BaseGameActionCommand command, List<string> messages)
        {
            return new CommandResult
            {
                Command = command,
                Result = CommandResultEnum.Exception,
                Messages = messages
            };
        }

        public static CommandResult Success(BaseGameActionCommand command)
        {
            return new CommandResult
            {
                Command = command,
                Result = CommandResultEnum.Success
            };
        }

        public static CommandResult Success(BaseGameActionCommand command, BaseGameActionCommand subsequentCommand)
        {
            return new CommandResult
            {
                Command = command,
                Result = CommandResultEnum.Success,
                SubsequentCommands = new List<BaseGameActionCommand> { subsequentCommand }
            };
        }

        public static CommandResult Success(BaseGameActionCommand command, IList<BaseGameActionCommand> subsequentCommands)
        {
            return new CommandResult
            {
                Command = command,
                Result = CommandResultEnum.Success,
                SubsequentCommands = subsequentCommands
            };
        }

        public static CommandResult Success(BaseGameActionCommand command, string message)
        {
            return Success(command, new List<string> { message });
        }

        public static CommandResult Success(BaseGameActionCommand command, string message, BaseGameActionCommand subsequentCommand)
        {
            var commandResult = new CommandResult
            {
                Command = command,
                Result = CommandResultEnum.Success,
                Messages = new List<string> { message }
            };
            
            if (subsequentCommand != null)
                commandResult.SubsequentCommands.Add(subsequentCommand);

            return commandResult;
        }

        public static CommandResult Success(BaseGameActionCommand command, List<string> messages)
        {
            return new CommandResult
            {
                Command = command,
                Result = CommandResultEnum.Success,
                Messages = messages
            };
        }
    }
}
