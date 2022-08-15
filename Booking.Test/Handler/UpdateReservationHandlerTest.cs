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
        public async void ShouldUpdate(Guid userId, Guid reservationId, DateTime from, DateTime to, string observations)
        {
            _queries.FindById(reservationId).Returns(GetReservation(userId));

            var command = new UpdateReservationCommand(userId, reservationId, from, to, observations);
            var handler = new UpdateReservationHandler(_uow, _repository, _queries, _config);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.True(result.IsValid);
        }

        [Theory]
        [MemberData(nameof(GetInvalidData))]
        public async void ShouldNotUpdate(Guid userId, Guid reservationId, DateTime from, DateTime to, string observations)
        {
            _queries.FindById(reservationId).Returns(GetReservation(userId));

            var command = new UpdateReservationCommand(userId, reservationId, from, to, observations);
            var handler = new UpdateReservationHandler(_uow, _repository, _queries, _config);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void ShouldNotUpdateNotFound()
        {
            var userId = Guid.NewGuid();
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
            var userId = Guid.NewGuid();
            var reservationId = Guid.NewGuid();
            var from = DateTime.Now.Date.AddDays(1);
            var to = DateTime.Now.Date.AddDays(1);
            var observations = "Observations";

            _queries.FindById(reservationId).Returns(GetReservation(userId, DateTime.Now.Date, DateTime.Now.Date.AddDays(1)));

            var command = new UpdateReservationCommand(userId, reservationId, from, to, observations);
            var handler = new UpdateReservationHandler(_uow, _repository, _queries, _config);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void ShouldNotUpdateToBeforeTodayWhenReservationAlreadyStarted()
        {
            var userId = Guid.NewGuid();
            var reservationId = Guid.NewGuid();
            var from = DateTime.Now.Date.AddDays(-2);
            var to = DateTime.Now.Date.AddDays(-1);
            var observations = "Observations";

            _queries.FindById(reservationId).Returns(GetReservation(userId, DateTime.Now.Date.AddDays(-2), DateTime.Now.Date.AddDays(1)));

            var command = new UpdateReservationCommand(userId, reservationId, from, to, observations);
            var handler = new UpdateReservationHandler(_uow, _repository, _queries, _config);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void ShouldNotUpdateAnotherReservationExists()
        {
            var userId = Guid.NewGuid();
            var reservationId = Guid.NewGuid();
            var from = DateTime.Now.Date.AddDays(1);
            var to = DateTime.Now.Date.AddDays(3);
            var observations = "Observations";

            _queries.FindById(reservationId).Returns(GetReservation(userId));
            _queries.FindByRoomAndRange(default, default, default).ReturnsForAnyArgs(GetReservations());

            var command = new UpdateReservationCommand(userId, reservationId, from, to, observations);
            var handler = new UpdateReservationHandler(_uow, _repository, _queries, _config);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result.IsValid);
        }

        public static Reservation GetReservation(Guid userId)
        {
            return GetReservation(userId, DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(1));
        }

        public static Reservation GetReservation(Guid userId, DateTime from, DateTime to)
        {
            return new Reservation(Guid.NewGuid(), Guid.NewGuid(), userId, from, to, "Observations");
        }

        public static IList<Reservation> GetReservations()
        {
            return new List<Reservation> { GetReservation(Guid.NewGuid()) };
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
                    DateTime.Now.Date.AddDays(-1),
                    DateTime.Now.Date.AddDays(-1),
                    null
                });

            data.Add(new object[]
                {
                    Guid.NewGuid(),
                    null,
                    DateTime.Now.Date.AddDays(-1),
                    DateTime.Now.Date.AddDays(-1),
                    null
                });

            data.Add(new object[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(1),
                    DateTime.Now.Date.AddDays(-1),
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
                    DateTime.Now.Date,
                    DateTime.Now.Date,
                    "ObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservationsObservati"
                });

            data.Add(new object[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(29),
                    DateTime.Now.Date.AddDays(31),
                    "Observations"
                });

            data.Add(new object[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(31),
                    DateTime.Now.Date.AddDays(33),
                    "Observations"
                });

            data.Add(new object[]
               {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.Now.Date.AddDays(1),
                    DateTime.Now.Date.AddDays(4),
                    "Observations"
               });

            return data;
        }
    }
}
