using System.Threading;
using System.Threading.Tasks;
using HotelAutomationApp.Domain.Models.Identity;

namespace HotelAutomationApp.Infrastructure.Interfaces.Auth.Services
{
    public interface ISecurityContext
    {
        bool UserExists { get;}
        
        string UserId { get; }

        Task<ApplicationUser?> GetCurrentUserAsync(CancellationToken cancellationToken);
    }
}