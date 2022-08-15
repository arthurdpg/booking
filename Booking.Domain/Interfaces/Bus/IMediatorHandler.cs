using Booking.Domain.Commands;
using FluentValidation.Results;

namespace Booking.Domain.Interfaces.Bus
{
    public interface IMediatorHandler
    {
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}
