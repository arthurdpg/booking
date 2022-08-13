using Booking.Application.ViewModels;
using FluentValidation.Results;

namespace Booking.Application.Interfaces
{
    public interface IReservationAppService
    {
        Task<ReservationViewModel> GetById(Guid id);
        Task<IList<ReservationViewModel>> GetByUserId(string id);
        Task<ValidationResult> Create(ManageReservationViewModel reservationViewModel);
        Task<ValidationResult> Update(Guid id, ManageReservationViewModel reservationViewModel);
        Task<ValidationResult> Delete(Guid id);
    }
}
