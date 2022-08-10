using MediatR;

namespace Booking.Domain.Commands
{
    public interface ICommand<out TResult> : IRequest<TResult>
        where TResult : CommandResult
    {

    }
}
