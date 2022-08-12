using Booking.Domain.Commands;
using Booking.Domain.Commands.Reservation;
using Booking.Domain.Interfaces;
using Booking.Domain.Interfaces.Queries;

namespace Booking.Domain.Handlers.Reservation
{
    public class UpdateReservationHandler : IHandler<UpdateReservationCommand, CommandResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Models.Reservation> _repository;
        private readonly IReservationQueries _queries;

        public UpdateReservationHandler(IUnitOfWork uow, IRepository<Models.Reservation> repository, IReservationQueries queries)
        {
            _uow = uow;
            _repository = repository;
            _queries = queries;
        }

        public async Task<CommandResult> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return CommandResultFactory.ValidationErrorResult(request);

            var reservation = await _queries.FindById(request.ReservationId);

            if (reservation == null)
                return CommandResultFactory.ErrorResult(Messages.NotFound);

            if (!reservation.UserId.Equals(request.UserId))
                return CommandResultFactory.ErrorResult(Messages.AccessDenied);

            if (request.From.Date < reservation.From.Date || request.To.Date > reservation.To.Date)
            {
                // Need to check the availability
            }

            // Create an update method
            reservation.From = request.From;
            reservation.To = request.To;
            reservation.Observations = request.Observations;

            _repository.Update(reservation);
            await _uow.CommitAsync();

            return CommandResultFactory.SuccessResult();
        }
    }
}
