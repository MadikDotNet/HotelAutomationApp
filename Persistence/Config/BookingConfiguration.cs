using HotelAutomationApp.Domain.Models.Bookings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelAutomationApp.Persistence.Config;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.OwnsOne(q => q.TotalPrice, w => w.Property(q => q.Value).HasColumnName("TotalPrice").IsRequired())
            .Navigation(q => q.TotalPrice);
    }
}