using HotelAutomation.Domain.Models.Rooms;
using HotelAutomation.Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder
                .OwnsOne(q => q.PricePerNight, price => price.Property(q => q.Value).HasColumnName(nameof(Price)).IsRequired())
                .Navigation(q => q.PricePerNight).IsRequired();
        }
    }
}