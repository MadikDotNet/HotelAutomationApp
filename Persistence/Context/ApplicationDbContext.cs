using System;
using System.Threading.Tasks;
using HotelAutomation.Domain.Models.Rooms;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using HotelAutomationApp.Persistence.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>, IDbContext
    {
        private readonly ISecurityContext _securityContext;
        
        public ApplicationDbContext(DbContextOptions options, ISecurityContext securityContext) : base(options)
        {
            _securityContext = securityContext;
        }
        
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        #region Room

        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomGroup> RoomGroups { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }

        #endregion
        
        public async Task<int> SaveChangesAsync()
        {
            AuditChanges();
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoomConfiguration());
            builder.ApplyConfiguration(new RoomGroupConfiguration());
        }

        private void AuditChanges()
        {
            var entities = ChangeTracker.Entries<AuditableEntity>();
            
            foreach (var entity in entities)
            {
                if (entity.State is EntityState.Added)
                {
                    entity.Entity.CreatedBy = _securityContext.UserId;
                    entity.Entity.CreationDate = DateTime.UtcNow;
                    entity.Entity.LastModifiedBy = _securityContext.UserId;
                    entity.Entity.LastModifiedDate = DateTime.UtcNow;
                }
                
                if (entity.State is EntityState.Modified)
                {
                    entity.Entity.LastModifiedBy = _securityContext.UserId;
                    entity.Entity.LastModifiedDate = DateTime.UtcNow;
                }
            }
        }
    }
}