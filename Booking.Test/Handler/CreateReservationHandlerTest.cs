using Booking.Domain.Commands.Reservation;
using Booking.Domain.Configuration;
using Booking.Domain.Handlers.Reservation;
using Booking.Domain.Interfaces;
using Booking.Domain.Interfaces.Queries;
using Booking.Domain.Models;
using NSubstitute;
using Xunit;

namespace Booking.Test.Handlers
{
    public class CreateReservationHandlerTest
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Reservation> _repository;
        private readonly IReservationQueries _queries;
        private readonly RulesConfig _config;

        public CreateReservationHandlerTest()
        {
            _uow = Substitute.For<IUnitOfWork>();
            _uow.CommitAsync().Returns(1);

            _repository = Substitute.For<IRepository<Reservation>>();
            _queries = Substitute.For<IReservationQueries>();
            _config = new RulesConfig { MaximumDaysInAdvance = 30, MaximumStayDuration = 3 };
        }

        [Theory]
        [MemberData(nameof(GetValidData))]
        public async void ShouldCreate(string userId, Guid roomId, DateTime from, DateTime to, string observations)
        {
            var command = new CreateReservationCommand(userId, roomId, from, to, observations);
            var handler = new CreateReservationHandler(_uow, _repository, _queries, _config);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.True(result.IsValid);
        }

        [Theory]
        [MemberData(nameof(GetInvalidData))]
        public async void ShouldNotCreate(string userId, Guid roomId, DateTime from, DateTime to, string observations)
        {
            var command = new CreateReservationCommand(userId, roomId, from, to, observations);
            var handler = new CreateReservationHandler(_uow, _repository, _queries, _config);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void ShouldNotCreateAnotherReservationExists()
        {
            var userId = "useremail@domain.com";
            var roomId = Guid.NewGuid();
            var from = DateTime.Now.Date.AddDays(1);
            var to = DateTime.Now.Date.AddDays(1);
            var observations = "Observations";

            _queries.FindByRoomAndRange(roomId, from, to).Returns(GetReservations());

            var command = new CreateReservationCommand(userId, roomId, from, to, observations);
            var handler = new CreateReservationHandler(_uow, _repository, _queries, _config);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result.IsValid);
        }

        public static IList<Reservation> GetReservations()
        {
            return new List<Reservation> { new Reservation(Guid.NewGuid(), Guid.NewGuid(), "useremail@domain.com", DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(1), "Observations") };
        }

        public static IEnumerable<object[]> GetValidData()
        {
            var data = new List<object[]>();

            data.Add(new object[]
                {
                    "useremail@domain.com",
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(1),
                    DateTime.Now.Date.AddDays(1),
                    null
                });

            data.Add(new object[]
              {
                    "useremail@domain.com",
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(1),
                    DateTime.Now.Date.AddDays(2),
                    null
              });

            data.Add(new object[]
                {
                    "useremail@domain.com",
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
                    "useremail@domain.com",
                    null,
                    DateTime.Now.Date,
                    DateTime.Now.Date,
                    null
                });

            data.Add(new object[]
                {
                    "useremail@domain.com",
                    Guid.NewGuid(),
                    DateTime.Now.Date,
                    DateTime.Now.Date,
                    null
                });

            data.Add(new object[]
                {
                    "useremail@domain.com",
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(1),
                    DateTime.Now.Date,
                    null
                });

            data.Add(new object[]
                {
                    "useremail@domain.com",
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(2),
                    DateTime.Now.Date.AddDays(1),
                    null
                });

            data.Add(new object[]
                {
                    "useremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailuseremailus@domain.com",
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(1),
                    DateTime.Now.Date.AddDays(1),
                    null
                });

            data.Add(new object[]
                {
                    "useremail@domain.com",
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(1),
                    DateTime.Now.Date.AddDays(1),
                    "ObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservati"
                });

            data.Add(new object[]
                {
                    "useremail@domain.com",
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(29),
                    DateTime.Now.Date.AddDays(31),
                    "Observations"
                });

            data.Add(new object[]
                {
                    "useremail@domain.com",
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(31),
                    DateTime.Now.Date.AddDays(33),
                    "Observations"
                });

            data.Add(new object[]
               {
                    "useremail@domain.com",
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(1),
                    DateTime.Now.Date.AddDays(4),
                    "Observations"
               });

            return data;
        }
    }
}
