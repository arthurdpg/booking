using AutoMapper;
using Booking.Application.ViewModels;
using Booking.Domain.Models;

namespace Booking.Application.Mapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<Facility, FacilityViewModel>();
            CreateMap<Room, RoomViewModel>();
        }
    }
}
