using Booking.Domain.Commands;
using Booking.Domain.Commands.Reservation;
using Booking.Domain.Configuration;
using Booking.Domain.Interfaces;
using Booking.Domain.Interfaces.Queries;

namespace Booking.Domain.Handlers.Reservation
{
    public class UpdateReservationHandler : IHandler<UpdateReservationCommand, CommandResult>
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

        public async Task<CommandResult> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return CommandResultFactory.ValidationErrorResult(request);

            var reservation = await _queries.FindById(request.ReservationId);

            if (reservation == null)
                return CommandResultFactory.ErrorResult(Messages.NotFound);

            if (!reservation.UserId.Equals(request.UserId))
                return CommandResultFactory.ErrorResult(Messages.AccessDenied);

            var maxDateInAdvance = DateTime.Now.Date.AddDays(_config.MaximumDaysInAdvance);
            if (request.From.Date > maxDateInAdvance || request.To.Date > maxDateInAdvance)
                return CommandResultFactory.ErrorResult(string.Format(Messages.MaximumDaysInAdvance, _config.MaximumDaysInAdvance));

            if (request.To.Date.Subtract(request.From.Date).Days > _config.MaximumStayDuration)
                return CommandResultFactory.ErrorResult(string.Format(Messages.MaximumStayDuration, _config.MaximumStayDuration));

            if (request.From.Date < reservation.From.Date || request.To.Date > reservation.To.Date)
            {
                // Needs to check the availability
                var reservations = await _queries.FindByRoomAndRange(reservation.RoomId, request.From.Date, request.To.Date);

                if (reservations.Any(r => r.Id != reservation.Id))
                    return CommandResultFactory.ErrorResult(Messages.ThereIsAnotherReservationSamePeriod);
            }

            reservation.Update(request.From.Date, request.To.Date, request.Observations);

            _repository.Update(reservation);
            await _uow.CommitAsync();

            return CommandResultFactory.SuccessResult();
        }
    }
}
