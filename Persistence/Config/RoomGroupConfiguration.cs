using HotelAutomation.Domain.Models.Rooms;
using HotelAutomation.Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelAutomationApp.Persistence.Config
{
    public class RoomGroupConfiguration : IEntityTypeConfiguration<RoomGroup>
    {
        public void Configure(EntityTypeBuilder<RoomGroup> builder)
        {
            builder
                .OwnsOne(q => q.Name, group => group.Property(q => q.Value).HasColumnName(nameof(Name)))
                .Navigation(q => q.Name);

            builder
                .OwnsOne(q => q.MinPrice, price => price.Property(q => q.Value).HasColumnName("MinPrice"))
                .Navigation(q => q.MinPrice);
        }
    }
}