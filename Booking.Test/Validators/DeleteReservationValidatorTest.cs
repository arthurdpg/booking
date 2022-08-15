using Booking.Domain.Commands.Reservation;
using Booking.Domain.Commands.Reservation.Validators;
using Xunit;

namespace Booking.Test.Validators
{
    public class DeleteReservationValidatorTest
    {
        [Theory]
        [MemberData(nameof(GetValidData))]
        public void ShouldBeValidCommand(string userId, Guid reservationId)
        {
            var command = new DeleteReservationCommand(userId, reservationId);
            var validator = new DeleteReservationValidator();
            var result = validator.Validate(command);
            Assert.True(result.IsValid);
        }

        [Theory]
        [MemberData(nameof(GetInvalidData))]
        public void ShouldBeInvalidCommand(string userId, Guid reservationId)
        {
            var command = new DeleteReservationCommand(userId, reservationId);
            var validator = new DeleteReservationValidator();
            var result = validator.Validate(command);
            Assert.False(result.IsValid);
        }

        public static IEnumerable<object[]> GetValidData()
        {
            var data = new List<object[]>();

            data.Add(new object[]
                {
                    "useremail@domain.com",
                    Guid.NewGuid()
                });

            return data;
        }

        public static IEnumerable<object[]> GetInvalidData()
        {
            var data = new List<object[]>();

            data.Add(new object[]
                {
                    null,
                    null
                });

            data.Add(new object[]
                {
                    null,
                    Guid.NewGuid()
                });

            data.Add(new object[]
               {
                    string.Empty,
                    Guid.NewGuid()
               });

            data.Add(new object[]
                {
                    "useremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailus@domain.com",
                    Guid.NewGuid()
                });

            return data;
        }
    }
}
