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
    public class UpdateReservationHandlerTest
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Reservation> _repository;
        private readonly IReservationQueries _queries;
        private readonly RulesConfig _config;

        public UpdateReservationHandlerTest()
        {
            _uow = Substitute.For<IUnitOfWork>();
            _uow.CommitAsync().Returns(1);

            _repository = Substitute.For<IRepository<Reservation>>();
            _queries = Substitute.For<IReservationQueries>();
            _config = new RulesConfig { MaximumDaysInAdvance = 30, MaximumStayDuration = 3 };
        }

        [Theory]
        [MemberData(nameof(GetValidData))]
        public async void ShouldUpdate(string userId, Guid reservationId, DateTime from, DateTime to, string observations)
        {
            _queries.FindById(reservationId).Returns(GetReservation());

            var command = new UpdateReservationCommand(userId, reservationId, from, to, observations);
            var handler = new UpdateReservationHandler(_uow, _repository, _queries, _config);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.True(result.IsValid);
        }

        [Theory]
        [MemberData(nameof(GetInvalidData))]
        public async void ShouldNotUpdate(string userId, Guid reservationId, DateTime from, DateTime to, string observations)
        {
            _queries.FindById(reservationId).Returns(GetReservation());

            var command = new UpdateReservationCommand(userId, reservationId, from, to, observations);
            var handler = new UpdateReservationHandler(_uow, _repository, _queries, _config);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void ShouldNotUpdateNotFound()
        {
            var userId = "useremail@domain.com";
            var reservationId = Guid.NewGuid();
            var from = DateTime.Now.Date.AddDays(1);
            var to = DateTime.Now.Date.AddDays(1);
            var observations = "Observations";

            _queries.FindById(reservationId).Returns((Reservation)null);

            var command = new UpdateReservationCommand(userId, reservationId, from, to, observations);
            var handler = new UpdateReservationHandler(_uow, _repository, _queries, _config);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void ShouldNotUpdatFromWhenReservationAlreadyStarted()
        {
            var userId = "useremail@domain.com";
            var reservationId = Guid.NewGuid();
            var from = DateTime.Now.Date.AddDays(1);
            var to = DateTime.Now.Date.AddDays(1);
            var observations = "Observations";

            _queries.FindById(reservationId).Returns(GetReservation(DateTime.Now.Date, DateTime.Now.Date.AddDays(1)));

            var command = new UpdateReservationCommand(userId, reservationId, from, to, observations);
            var handler = new UpdateReservationHandler(_uow, _repository, _queries, _config);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void ShouldNotUpdateToBeforeTodayWhenReservationAlreadyStarted()
        {
            var userId = "useremail@domain.com";
            var reservationId = Guid.NewGuid();
            var from = DateTime.Now.Date.AddDays(-2);
            var to = DateTime.Now.Date.AddDays(-1);
            var observations = "Observations";

            _queries.FindById(reservationId).Returns(GetReservation(DateTime.Now.Date.AddDays(-2), DateTime.Now.Date.AddDays(1)));

            var command = new UpdateReservationCommand(userId, reservationId, from, to, observations);
            var handler = new UpdateReservationHandler(_uow, _repository, _queries, _config);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void ShouldNotUpdateAnotherReservationExists()
        {
            var userId = "useremail@domain.com";
            var reservationId = Guid.NewGuid();
            var from = DateTime.Now.Date.AddDays(1);
            var to = DateTime.Now.Date.AddDays(3);
            var observations = "Observations";

            _queries.FindById(reservationId).Returns(GetReservation());
            _queries.FindByRoomAndRange(default, default, default).ReturnsForAnyArgs(GetReservations());

            var command = new UpdateReservationCommand(userId, reservationId, from, to, observations);
            var handler = new UpdateReservationHandler(_uow, _repository, _queries, _config);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result.IsValid);
        }

        public static Reservation GetReservation()
        {
            return GetReservation(DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(1));
        }

        public static Reservation GetReservation(DateTime from, DateTime to)
        {
            return new Reservation(Guid.NewGuid(), Guid.NewGuid(), "useremail@domain.com", from, to, "Observations");
        }

        public static IList<Reservation> GetReservations()
        {
            return new List<Reservation> { GetReservation() };
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
