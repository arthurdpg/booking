using Booking.Domain.Commands;

namespace Booking.Domain.Interfaces.Bus
{
    public interface IMediatorHandler
    {
        Task<CommandResult> SendCommand<T>(T command) where T : ICommand<CommandResult>;
    }
}
