using System.Data;
using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.MediaFiles;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Domain.Models.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HotelAutomationApp.Persistence.Interfaces.Context
{
    public interface IApplicationDbContext
    {
        #region Room

        DbSet<ApplicationUser> User { get; set; }
        DbSet<Room> Room { get; set; }
        DbSet<RoomGroup> RoomGroup { get; set; }
        DbSet<RoomMedia> RoomMedia { get; set; }
        DbSet<Media> Media { get; set; }

        #endregion

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IQueryable<TEntity> AsQueryable<TEntity>() where TEntity : BaseEntity;
        DbSet<TEntity> AsDbSet<TEntity>() where TEntity : BaseEntity;
        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task CommitTransactionAsync();
    }
}