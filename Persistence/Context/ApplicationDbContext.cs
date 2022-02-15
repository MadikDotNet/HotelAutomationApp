using System.Data;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Domain.Models.MediaFiles;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Domain.Models.Services;
using HotelAutomationApp.Persistence.Config;
using HotelAutomationApp.Persistence.Interfaces.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HotelAutomationApp.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        #region Identity

        public DbSet<ApplicationUser> User { get; set; }

        #endregion

        #region Room

        public DbSet<Room> Room { get; set; }
        public DbSet<RoomGroup> RoomGroup { get; set; }
        public DbSet<RoomMedia> RoomMedia { get; set; }

        #endregion

        #region MediaFiles

        public DbSet<Media> Media { get; set; }

        #endregion

        #region Services

        public DbSet<Service> Service { get; set; }

        #endregion

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AuditChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<TEntity> AsDbSet<TEntity>()
            where TEntity : BaseEntity =>
            Set<TEntity>();

        public async Task<IDbContextTransaction> BeginTransactionAsync(
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) =>
            await Database.BeginTransactionAsync(isolationLevel);

        public async Task CommitTransactionAsync() =>
            await Database.CommitTransactionAsync();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoomConfiguration());
            builder.ApplyConfiguration(new RoomGroupConfiguration());
            builder.ApplyConfiguration(new ServiceConfiguration());
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