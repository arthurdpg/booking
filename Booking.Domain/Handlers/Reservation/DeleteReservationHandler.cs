using Booking.Domain.Commands.Reservation;
using Booking.Domain.Interfaces;
using Booking.Domain.Interfaces.Queries;
using FluentValidation.Results;
using MediatR;

namespace Booking.Domain.Handlers.Reservation
{
    public class DeleteReservationHandler : CommandHandler, IRequestHandler<DeleteReservationCommand, ValidationResult>
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

        public async Task<ValidationResult> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            var reservation = await _queries.FindById(request.ReservationId);

            if (reservation == null)
                return ErrorResult(Messages.NotFound);

            if (reservation.UserId != request.UserId)
                return ErrorResult(Messages.AccessDenied);

            if (reservation.From.Date <= DateTime.Now.Date)
                return ErrorResult(Messages.ReservationDeteleNotAllowed);

            _repository.Delete(reservation);

            return await Commit(_uow);
        }
    }
}
