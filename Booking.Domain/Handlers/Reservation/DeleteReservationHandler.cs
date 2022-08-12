using Booking.Domain.Commands;
using Booking.Domain.Commands.Reservation;
using Booking.Domain.Interfaces;
using Booking.Domain.Interfaces.Queries;

namespace Booking.Domain.Handlers.Reservation
{
    public class DeleteReservationHandler : IHandler<DeleteReservationCommand, CommandResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Models.Reservation> _repository;
        private readonly IReservationQueries _queries;

        public DeleteReservationHandler(IUnitOfWork uow, IRepository<Models.Reservation> repository, IReservationQueries queries)
        {
            _uow = uow;
            _repository = repository;
            _queries = queries;
        }

        public async Task<CommandResult> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return CommandResultFactory.ValidationErrorResult(request);

            var reservation = await _queries.FindById(request.ReservationId);

            if (reservation == null)
                return CommandResultFactory.ErrorResult(Messages.NotFound);

            _repository.Delete(reservation);
            await _uow.CommitAsync();

            return CommandResultFactory.SuccessResult();
        }
    }
}
