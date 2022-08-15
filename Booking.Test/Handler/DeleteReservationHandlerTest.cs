using Booking.Domain.Commands.Reservation;
using Booking.Domain.Handlers.Reservation;
using Booking.Domain.Interfaces;
using Booking.Domain.Interfaces.Queries;
using Booking.Domain.Models;
using NSubstitute;
using Xunit;

namespace Booking.Test.Handlers
{
    public class DeleteReservationHandlerTest
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Reservation> _repository;
        private readonly IReservationQueries _queries;

        public DeleteReservationHandlerTest()
        {
            _uow = Substitute.For<IUnitOfWork>();
            _uow.CommitAsync().Returns(1);

            _repository = Substitute.For<IRepository<Reservation>>();
            _queries = Substitute.For<IReservationQueries>();
        }

        [Theory]
        [MemberData(nameof(GetValidData))]
        public async void ShouldDelete(Guid userId, Guid reservationId)
        {
            _queries.FindById(reservationId).Returns(GetReservation(userId));

            var command = new DeleteReservationCommand(userId, reservationId);
            var handler = new DeleteReservationHandler(_uow, _repository, _queries);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.True(result.IsValid);
        }

        [Theory]
        [MemberData(nameof(GetInvalidData))]
        public async void ShouldNotDelete(Guid userId, Guid reservationId)
        {
            _queries.FindById(reservationId).Returns(GetReservation(userId));

            var command = new DeleteReservationCommand(userId, reservationId);
            var handler = new DeleteReservationHandler(_uow, _repository, _queries);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void ShouldNotDeleteNotFound()
        {
            var userId = Guid.NewGuid();
            var reservationId = Guid.NewGuid();
            _queries.FindById(reservationId).Returns((Reservation)null);

            var command = new DeleteReservationCommand(userId, reservationId);
            var handler = new DeleteReservationHandler(_uow, _repository, _queries);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void ShouldNotDeleteNotAllowed()
        {
            var userId = Guid.NewGuid();
            var reservationId = Guid.NewGuid();
            _queries.FindById(reservationId).Returns(GetReservation(Guid.NewGuid()));

            var command = new DeleteReservationCommand(userId, reservationId);
            var handler = new DeleteReservationHandler(_uow, _repository, _queries);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void ShouldNotDeleteReservationAlreadyStarted()
        {
            var userId = Guid.NewGuid();
            var reservationId = Guid.NewGuid();
            _queries.FindById(reservationId).Returns(GetStartedReservation());

            var command = new DeleteReservationCommand(userId, reservationId);
            var handler = new DeleteReservationHandler(_uow, _repository, _queries);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result.IsValid);
        }

        public static Reservation GetReservation(Guid userId)
        {
            return new Reservation(Guid.NewGuid(), Guid.NewGuid(), userId, DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(1), "Observations");
        }

        public static Reservation GetStartedReservation()
        {
            return new Reservation(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.Date, DateTime.Now.Date.AddDays(1), "Observations");
        }

        public static IEnumerable<object[]> GetValidData()
        {
            var data = new List<object[]>();

            data.Add(new object[]
                {
                    Guid.NewGuid(),
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
                    Guid.NewGuid(),
                    null
               });

            return data;
        }
    }
}
