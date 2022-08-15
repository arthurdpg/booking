using AutoMapper;
using Booking.Application.Interfaces;
using Booking.Application.ViewModels;
using Booking.Domain.Commands.Reservation;
using Booking.Domain.Interfaces.Bus;
using Booking.Domain.Interfaces.Queries;
using FluentValidation.Results;

namespace Booking.Application.Services
{
    public class ReservationAppService : IReservationAppService
    {
        private readonly IMapper _mapper;
        private readonly IReservationQueries _reservationQueries;
        private readonly IMediatorHandler _mediator;

        public ReservationAppService(IMapper mapper, IReservationQueries reservationQueries, IMediatorHandler mediator)
        {
            _mapper = mapper;
            _reservationQueries = reservationQueries;
            _mediator = mediator;
        }

        public async Task<IList<ReservationViewModel>> GetByUserId(string userId)
        {
            return _mapper.Map<IList<ReservationViewModel>>(await _reservationQueries.FindByUserId(userId));
        }

        public async Task<ReservationViewModel> GetByUserReservationId(string userId, Guid reservationId)
        {
            return _mapper.Map<ReservationViewModel>(await _reservationQueries.FindByUserReservationId(userId, reservationId));
        }

        public async Task<ValidationResult> Create(ManageReservationViewModel model)
        {
            var createCommand = _mapper.Map<CreateReservationCommand>(model);
            return await _mediator.SendCommand(createCommand);
        }

        public async Task<ValidationResult> Update(Guid id, ManageReservationViewModel model)
        {
            var updateCommand = new UpdateReservationCommand(model.UserId, id, model.From, model.To, model.Observations);
            return await _mediator.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Delete(string userId, Guid reservationId)
        {
            var removeCommand = new DeleteReservationCommand(userId, reservationId);
            return await _mediator.SendCommand(removeCommand);
        }
    }
}
