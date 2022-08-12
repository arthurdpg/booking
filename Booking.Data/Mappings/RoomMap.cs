using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Data.Mappings
{
    internal class RoomMap : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.Size)
                .IsRequired();

            builder.HasOne(x => x.Hotel)
                .WithMany(x => x.Rooms)
                .HasForeignKey(x => x.HotelId)
                .IsRequired();

            builder.HasMany(x => x.Facilities).WithMany(x => x.Rooms);
        }
    }
}
