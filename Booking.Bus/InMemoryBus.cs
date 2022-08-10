using Booking.Domain.Commands;
using Booking.Domain.Interfaces.Bus;
using MediatR;

namespace Booking.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<CommandResult> SendCommand<T>(T command) where T : ICommand<CommandResult>
        {
            return _mediator.Send(command);
        }
    }
}
