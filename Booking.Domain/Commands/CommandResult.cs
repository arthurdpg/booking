namespace Booking.Domain.Commands
{
    public class CommandResult
    {
        public CommandResult(bool isValid)
        {
            IsValid = isValid;
        }

        public bool IsValid { get; private set; }
        private List<string> Messages { get; } = new List<string>();

        public void AddMessage(string message)
        {
            Messages.Add(message);
        }

        public IReadOnlyList<string> ListMessages()
        {
            return Messages;
        }
    }
}
