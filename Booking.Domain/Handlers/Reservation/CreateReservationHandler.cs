using Booking.Domain.Commands;
using Booking.Domain.Commands.Reservation;
using Booking.Domain.Configuration;
using Booking.Domain.Interfaces;
using Booking.Domain.Interfaces.Queries;

namespace Booking.Domain.Handlers.Reservation
{
    public class CreateReservationHandler : IHandler<CreateReservationCommand, CommandResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Models.Reservation> _repository;
        private readonly IReservationQueries _queries;
        private readonly RulesConfig _config;

        public CreateReservationHandler(IUnitOfWork uow, IRepository<Models.Reservation> repository, IReservationQueries queries, RulesConfig config)
        {
            _uow = uow;
            _repository = repository;
            _queries = queries;
            _config = config;
        }

        public async Task<CommandResult> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return CommandResultFactory.ValidationErrorResult(request);

            var maxDateInAdvance = DateTime.Now.Date.AddDays(_config.MaximumDaysInAdvance);
            if (request.From.Date > maxDateInAdvance || request.To.Date > maxDateInAdvance)
                return CommandResultFactory.ErrorResult(string.Format(Messages.MaximumDaysInAdvance, _config.MaximumDaysInAdvance));

            if (request.To.Date.Subtract(request.From.Date).Days > _config.MaximumStayDuration)
                return CommandResultFactory.ErrorResult(string.Format(Messages.MaximumStayDuration, _config.MaximumStayDuration));

            var reservations = await _queries.FindByRoomAndRange(request.RoomId, request.From.Date, request.To.Date);
            
            if(reservations.Any())
                return CommandResultFactory.ErrorResult(Messages.ThereIsAnotherReservationSamePeriod);

            var reservation = new Models.Reservation(Guid.NewGuid(), request.RoomId, request.UserId, request.From.Date, request.To.Date, request.Observations);

            await _repository.InsertAsync(reservation);
            await _uow.CommitAsync();

            return CommandResultFactory.SuccessResult();
        }
    }
}
