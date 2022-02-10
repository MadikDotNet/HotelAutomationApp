using System.Data;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Domain.Models.MediaFiles;
using HotelAutomationApp.Domain.Models.Rooms;
using HotelAutomationApp.Domain.Models.Services;
using Microsoft.EntityFrameworkCore;
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
        DbSet<RoomMedia> RoomMedia { get; set; }

        #endregion

        #region MediaFiles

        DbSet<Media> Media { get; set; }

        #endregion

        #region Services

        DbSet<Service> Service { get; set; }

        #endregion

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IQueryable<TEntity> AsQueryable<TEntity>() where TEntity : BaseEntity;
        DbSet<TEntity> AsDbSet<TEntity>() where TEntity : BaseEntity;
        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task CommitTransactionAsync();
    }
}