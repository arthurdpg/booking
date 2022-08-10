namespace Booking.Domain.Commands
{
    public static class CommandResultFactory
    {
        public static CommandResult None()
        {
            return new CommandResult(true);
        }

        public static CommandResult SuccessResult(params string[] messages)
        {
            var result = new CommandResult(true);
            foreach (var message in messages)
                result.AddMessage(message);

            return result;
        }

        public static CommandResult ValidationErrorResult(Command command)
        {
            var result = new CommandResult(false);
            foreach (var error in command.ValidationResult.Errors)
                result.AddMessage(error.ErrorMessage);

            return result;
        }

        public static CommandResult ErrorResult(params string[] errors)
        {
            var result = new CommandResult(false);
            foreach (var error in errors)
                result.AddMessage(error);

            return result;
        }
    }
}

