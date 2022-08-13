using Booking.Application.ViewModels;
using FluentValidation.Results;

namespace Booking.Application.Interfaces
{
    public interface IReservationAppService
    {
        Task<ReservationViewModel> GetById(Guid id);
        Task<ValidationResult> Create(ReservationViewModel reservationViewModel);
        Task<ValidationResult> Update(ReservationViewModel reservationViewModel);
        Task<ValidationResult> Delete(Guid id);
    }
}
