using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Data.Mappings
{
    internal class FacilityMap : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255)
                .IsRequired(true);
        }
    }
}
