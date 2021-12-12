using System.Threading;
using System.Threading.Tasks;

namespace HotelAutomationApp.Infrastructure.Interfaces.Auth.Services
{
    public interface ISecurityContext
    {
        bool UserExists { get;}
        
        string UserId { get; }
        
        Task<User?> GetUserAsync(CancellationToken cancellationToken);
    }
}