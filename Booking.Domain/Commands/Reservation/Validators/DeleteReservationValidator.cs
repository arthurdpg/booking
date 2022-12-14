using FluentValidation;

namespace Booking.Domain.Commands.Reservation.Validators
{
    public class DeleteReservationValidator : AbstractValidator<DeleteReservationCommand>
    {
        public DeleteReservationValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(ValidationMessages.Required);

            RuleFor(x => x.ReservationId)
                .NotEmpty().WithMessage(ValidationMessages.Required);
        }
    }
}
