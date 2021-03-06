using System.Data;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Common.Abstractions.Audition;
using HotelAutomationApp.Domain.Models.Bookings;
using HotelAutomationApp.Domain.Models.BookingServices;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Domain.Models.MediaFiles;
using HotelAutomationApp.Domain.Models.Messaging;
using HotelAutomationApp.Domain.Models.Messaging.Appeals;
using HotelAutomationApp.Domain.Models.RoomGroups;
using HotelAutomationApp.Domain.Models.RoomGroupServices;
using HotelAutomationApp.Domain.Models.RoomMediaFiles;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Domain.Models.ServiceGroups;
using HotelAutomationApp.Domain.Models.Services;
using HotelAutomationApp.Persistence.Interfaces.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        public DbSet<RoomFile> RoomFiles { get; set; }

        #endregion

        #region MediaFiles

        public DbSet<FileMetadata> FileMetadata { get; set; }

        #endregion

        #region Services

        public DbSet<RoomGroupService> RoomGroupService { get; set; }

        public DbSet<Service> Service { get; set; }

        public DbSet<BookingService> BookingService { get; set; }

        public DbSet<ServiceGroup> ServiceGroup { get; set; }

        #endregion

        #region Booking

        public DbSet<Booking> Booking { get; set; }

        #endregion

        #region Messaging

        public DbSet<Appeal> Appeal { get; set; }

        public DbSet<Feedback> Feedback { get; set; }

        #endregion

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AuditChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<TEntity> AsDbSet<TEntity>()
            where TEntity : BaseEntity =>
            Set<TEntity>();

        public EntityEntry<TEntity> AsEntry<TEntity>(TEntity entity)
            where TEntity : BaseEntity =>
            Entry(entity);

        public IEnumerable<EntityEntry<TEntity>> AsEntryRange<TEntity>()
            where TEntity : BaseEntity =>
            ChangeTracker.Entries<TEntity>();

        public async Task<IDbContextTransaction> BeginTransactionAsync(
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) =>
            await Database.BeginTransactionAsync(isolationLevel);

        public async Task CommitTransactionAsync() =>
            await Database.CommitTransactionAsync();

        private void AuditChanges()
        {
            var entities = ChangeTracker.Entries<IAuditable>();

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