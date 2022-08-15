using Booking.Application.ViewModels;
using FluentValidation.Results;

namespace Booking.Application.Interfaces
{
    public interface IReservationAppService
    {
        Task<IList<ReservationViewModel>> GetByUserId(Guid userId);
        Task<ReservationViewModel> GetByUserReservationId(Guid userId, Guid reservationId);
        Task<ValidationResult> Create(ManageReservationViewModel model);
        Task<ValidationResult> Update(Guid reservationId, ManageReservationViewModel model);
        Task<ValidationResult> Delete(Guid userId, Guid reservationId);
    }
}
