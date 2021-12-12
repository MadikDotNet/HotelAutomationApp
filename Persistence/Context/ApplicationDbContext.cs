using System;
using System.Threading.Tasks;
using HotelAutomation.Domain.Common;
using HotelAutomation.Domain.Models.Identity;
using HotelAutomation.Domain.Models.Rooms;
using HotelAutomation.Infrastructure.Auth.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Config;
using Persistence.Interfaces.Context;

namespace Persistence.Context
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