using Booking.Domain.Commands;
using Booking.Domain.Commands.Reservation;
using Booking.Domain.Interfaces;

namespace Booking.Domain.Handlers.Reservation
{
    public class CreateReservationHandler : IHandler<CreateReservationCommand, CommandResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Models.Reservation> _repository;

        public CreateReservationHandler(IUnitOfWork uow, IRepository<Models.Reservation> repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public async Task<CommandResult> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return CommandResultFactory.ValidationErrorResult(request);

            // Need to check the availability
            var reservation = new Models.Reservation();

            await _repository.InsertAsync(reservation);
            await _uow.CommitAsync();

            return CommandResultFactory.SuccessResult();
        }
    }
}
