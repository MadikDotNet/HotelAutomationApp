using HotelAutomationApp.Domain.Models.Services;
using HotelAutomationApp.Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelAutomationApp.Persistence.Config;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.OwnsOne(q => q.Name, service => service.Property(q => q.Value).HasColumnName(nameof(Name)))
            .Navigation(q => q.Name);
    }
}