using Booking.Domain.Commands;
using Booking.Domain.Commands.Reservation;
using Booking.Domain.Configuration;
using Booking.Domain.Interfaces;
using Booking.Domain.Interfaces.Queries;
using FluentValidation.Results;
using MediatR;

namespace Booking.Domain.Handlers.Reservation
{
    public class UpdateReservationHandler : CommandHandler, IRequestHandler<UpdateReservationCommand, ValidationResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Models.Reservation> _repository;
        private readonly IReservationQueries _queries;
        private readonly RulesConfig _config;

        public UpdateReservationHandler(IUnitOfWork uow, IRepository<Models.Reservation> repository, IReservationQueries queries, RulesConfig config)
        {
            _uow = uow;
            _repository = repository;
            _queries = queries;
            _config = config;
        }

        public async Task<ValidationResult> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            var reservation = await _queries.FindById(request.ReservationId);

            if (reservation == null)
                return ErrorResult(Messages.NotFound);

            if (!reservation.UserId.Equals(request.UserId))
                return ErrorResult(Messages.AccessDenied);

            var maxDateInAdvance = DateTime.Now.Date.AddDays(_config.MaximumDaysInAdvance);
            if (request.From.Date > maxDateInAdvance || request.To.Date > maxDateInAdvance)
                return ErrorResult(string.Format(Messages.MaximumDaysInAdvance, _config.MaximumDaysInAdvance));

            if (request.To.Date.Subtract(request.From.Date).Days >= _config.MaximumStayDuration)
                return ErrorResult(string.Format(Messages.MaximumStayDuration, _config.MaximumStayDuration));

            if (reservation.From.Date >= DateTime.Now.Date && (reservation.From.Date != request.From.Date))
                return ErrorResult(Messages.ReservationUpdateNotAllowed);

            if (reservation.From.Date >= DateTime.Now.Date && request.To < DateTime.Now.Date)
                return ErrorResult(Messages.ReservationUpdateNotAllowed);

            if (request.From.Date < reservation.From.Date || request.To.Date > reservation.To.Date)
            {
                // Needs to check the availability
                var reservations = await _queries.FindByRoomAndRange(reservation.RoomId, request.From.Date, request.To.Date);

                if (reservations.Any(r => r.Id != reservation.Id))
                    return ErrorResult(Messages.ThereIsAnotherReservationSamePeriod);
            }

            reservation.Update(request.From.Date, request.To.Date, request.Observations);

            _repository.Update(reservation);

            return await Commit(_uow);
        }
    }
}
