using Booking.Application.ViewModels;
using FluentValidation.Results;

namespace Booking.Application.Interfaces
{
    public interface IReservationAppService
    {
        Task<IList<ReservationViewModel>> GetByUserId(string userId);
        Task<ReservationViewModel> GetByUserReservationId(string userId, Guid reservationId);
        Task<ValidationResult> Create(ManageReservationViewModel model);
        Task<ValidationResult> Update(Guid id, ManageReservationViewModel model);
        Task<ValidationResult> Delete(string userId, Guid reservationId);
    }
}
