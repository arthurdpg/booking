using System.ComponentModel.DataAnnotations;

namespace Booking.Application.ViewModels
{
    public class ReservationViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        public Guid RoomId { get; set; }

        [Required(ErrorMessage = "The E-mail is Required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "The E-mail is Required")]
        public DateTime From { get; set; }

        [Required(ErrorMessage = "The E-mail is Required")]
        public DateTime To { get; set; }

        public string Observations { get; set; }
    }
}
