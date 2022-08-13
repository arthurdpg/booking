using AutoMapper;
using Booking.Application.ViewModels;
using Booking.Domain.Commands.Reservation;

namespace Booking.Application.Mapper
{
    public class ViewModelToCommandProfile : Profile
    {
        public ViewModelToCommandProfile()
        {
            CreateMap<ReservationViewModel, CreateReservationCommand>()
                .ConstructUsing(x => new CreateReservationCommand(x.UserId, x.RoomId, x.From, x.To, x.Observations));

            CreateMap<ReservationViewModel, UpdateReservationCommand>()
                .ConstructUsing(x => new UpdateReservationCommand(x.UserId, x.Id, x.From, x.To, x.Observations));
        }
    }
}
