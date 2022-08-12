using Booking.Domain.Commands;
using Booking.Domain.Commands.Reservation;
using Booking.Domain.Interfaces;
using Booking.Domain.Interfaces.Bus;

namespace Booking.Domain.Handlers.Reservation
{
    public class CreateReservationHandler : IHandler<CreateReservationCommand, CommandResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Models.Reservation> _repository;
        private readonly IMediatorHandler _bus;

        public CreateReservationHandler(IUnitOfWork uow, IRepository<Models.Reservation> repository, IMediatorHandler bus)
        {
            _uow = uow;
            _repository = repository;
            _bus = bus;
        }

        public async Task<CommandResult> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return CommandResultFactory.ValidationErrorResult(request);

            return CommandResultFactory.SuccessResult();
        }
    }
}
