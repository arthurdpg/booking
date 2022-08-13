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

        public async Task<ReservationViewModel> GetById(Guid id)
        {
            return _mapper.Map<ReservationViewModel>(await _reservationQueries.FindById(id));
        }

        public async Task<ValidationResult> Create(ReservationViewModel reservationViewModel)
        {
            var createCommand = _mapper.Map<CreateReservationCommand>(reservationViewModel);
            return await _mediator.SendCommand(createCommand);
        }

        public async Task<ValidationResult> Delete(Guid id)
        {
            var removeCommand = new DeleteReservationCommand(id);
            return await _mediator.SendCommand(removeCommand);
        }

        public async Task<ValidationResult> Update(ReservationViewModel reservationViewModel)
        {
            var updateCommand = _mapper.Map<UpdateReservationCommand>(reservationViewModel);
            return await _mediator.SendCommand(updateCommand);
        }
    }
}
