using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.MediaFiles;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Persistence.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces.Context;

namespace HotelAutomationApp.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>,  IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        #region Room

        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomGroup> RoomGroup { get; set; }
        public DbSet<RoomMedia> RoomMedia { get; set; }
        public DbSet<Media> Media { get; set; }

        #endregion

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            AuditChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<TEntity> AsQueryable<TEntity>()
            where TEntity : BaseEntity =>
            Set<TEntity>().AsQueryable();

        public DbSet<TEntity> AsDbSet<TEntity>()
            where TEntity : BaseEntity =>
            Set<TEntity>();

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
                    entity.Entity.CreationDate = DateTime.UtcNow;
                    entity.Entity.LastModifiedDate = DateTime.UtcNow;
                }

                if (entity.State is EntityState.Modified)
                {
                    entity.Entity.LastModifiedDate = DateTime.UtcNow;
                }
            }
        }
    }
}