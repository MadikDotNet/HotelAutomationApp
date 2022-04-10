using System.Data;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Models.Bookings;
using HotelAutomationApp.Domain.Models.BookingServices;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Domain.Models.MediaFiles;
using HotelAutomationApp.Domain.Models.RoomGroups;
using HotelAutomationApp.Domain.Models.RoomGroupServices;
using HotelAutomationApp.Domain.Models.RoomMediaFiles;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Domain.Models.ServiceGroups;
using HotelAutomationApp.Domain.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace HotelAutomationApp.Persistence.Interfaces.Context
{
    public interface IApplicationDbContext
    {
        #region Identity

        DbSet<ApplicationUser> User { get; set; }

        #endregion

        #region Room

        DbSet<Room> Room { get; set; }
        DbSet<RoomGroup> RoomGroup { get; set; }
        DbSet<RoomFile> RoomFiles { get; set; }

        #endregion

        #region MediaFiles

        DbSet<FileMetadata> FileMetadata { get; set; }

        #endregion

        #region Services

        DbSet<RoomGroupService> RoomGroupService { get; set; }

        DbSet<Service> Service { get; set; }
        
        DbSet<BookingService> BookingService { get; set; }
        
        DbSet<ServiceGroup> ServiceGroup { get; set; }

        #endregion

        #region Boogings

        public DbSet<Booking> Booking { get; set; }

        #endregion

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> AsDbSet<TEntity>() where TEntity : BaseEntity;
        EntityEntry<TEntity> AsEntry<TEntity>(TEntity entity) where TEntity : BaseEntity;
        public IEnumerable<EntityEntry<TEntity>> AsEntryRange<TEntity>() where TEntity : BaseEntity;
        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task CommitTransactionAsync();
    }
}