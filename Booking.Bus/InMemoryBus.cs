using Booking.Domain.Commands;
using Booking.Domain.Interfaces.Bus;
using FluentValidation.Results;
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

        public Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }
    }
}
