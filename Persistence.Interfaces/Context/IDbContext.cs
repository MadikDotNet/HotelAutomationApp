using HotelAutomationApp.Domain.Common;
using HotelAutomationApp.Domain.Models.Identity;
using HotelAutomationApp.Domain.Models.Rooms;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Interfaces.Context
{
    public interface IDbContext
    {
        #region Room

        DbSet<User> Users { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<RoomGroup> RoomGroups { get; set; }
        DbSet<RoomImage> RoomImages { get; set; }

        #endregion

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IQueryable<TEntity> AsQueryable<TEntity>() where TEntity : BaseEntity;
        DbSet<TEntity> AsDbSet<TEntity>() where TEntity : BaseEntity;
    }
}