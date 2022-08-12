using Booking.Data.Mappings;
using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Data.Contexts
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HotelMap());
            modelBuilder.ApplyConfiguration(new RoomMap());
            modelBuilder.ApplyConfiguration(new FacilityMap());
            modelBuilder.ApplyConfiguration(new ReservationMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}