using Booking.Domain.Commands;
using MediatR;

namespace Booking.Domain.Handlers
{
    public interface IHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
        where TResult : CommandResult
    {

    }
}
