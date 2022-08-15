using Booking.Domain.Commands.Reservation;
using Booking.Domain.Commands.Reservation.Validators;
using Xunit;

namespace Booking.Test.Validators
{
    public class CreateReservationValidatorTest
    {
        [Theory]
        [MemberData(nameof(GetValidData))]
        public void ShouldBeValidCommand(Guid userId, Guid roomId, DateTime from, DateTime to, string observations)
        {
            var command = new CreateReservationCommand(userId, roomId, from, to, observations);
            var validator = new CreateReservationValidator();
            var result = validator.Validate(command);
            Assert.True(result.IsValid);
        }

        [Theory]
        [MemberData(nameof(GetInvalidData))]
        public void ShouldBeInvalidCommand(Guid userId, Guid roomId, DateTime from, DateTime to, string observations)
        {
            var command = new CreateReservationCommand(userId, roomId, from, to, observations);
            var validator = new CreateReservationValidator();
            var result = validator.Validate(command);
            Assert.False(result.IsValid);
        }

        public static IEnumerable<object[]> GetValidData()
        {
            var data = new List<object[]>();

            data.Add(new object[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(1),
                    DateTime.Now.Date.AddDays(1),
                    null
                });

            data.Add(new object[]
              {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(1),
                    DateTime.Now.Date.AddDays(2),
                    null
              });

            data.Add(new object[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(1),
                    DateTime.Now.Date.AddDays(1),
                    "Observations"
                });

            return data;
        }

        public static IEnumerable<object[]> GetInvalidData()
        {
            var data = new List<object[]>();

            data.Add(new object[]
                {
                    null,
                    null,
                    DateTime.Now.Date,
                    DateTime.Now.Date,
                    null
                });

            data.Add(new object[]
                {
                    Guid.NewGuid(),
                    null,
                    DateTime.Now.Date,
                    DateTime.Now.Date,
                    null
                });

            data.Add(new object[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.Now.Date,
                    DateTime.Now.Date,
                    null
                });

            data.Add(new object[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(1),
                    DateTime.Now.Date,
                    null
                });

            data.Add(new object[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(2),
                    DateTime.Now.Date.AddDays(1),
                    null
                });

            data.Add(new object[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(1),
                    DateTime.Now.Date.AddDays(1),
                    "ObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservati"
                });

            data.Add(new object[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.Now.Date,
                    DateTime.Now.Date.AddDays(1),
                    null
                });

            data.Add(new object[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(-1),
                    DateTime.Now.Date.AddDays(1),
                    null
                });

            return data;
        }
    }
}
