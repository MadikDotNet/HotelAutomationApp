using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelAutomationApp.Persistence.Config
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder
                .OwnsOne(q => q.PricePerHour, price => price.Property(q => q.Value).HasColumnName(nameof(Price)).IsRequired())
                .Navigation(q => q.PricePerHour).IsRequired();
        }
    }
}