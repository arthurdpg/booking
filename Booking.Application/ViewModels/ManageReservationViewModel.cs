using System.ComponentModel.DataAnnotations;

namespace Booking.Application.ViewModels
{
    public class ManageReservationViewModel
    {
        [Required(ErrorMessage = ValidationMessages.Required)]
        public Guid RoomId { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        public DateTime From { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        public DateTime To { get; set; }

        public string Observations { get; set; }
    }
}
