using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace HotelAutomationApp.Application
{
    public class PlugRequest : IRequest<string>
    {
       private class Handler : IRequestHandler<PlugRequest, string>
        {
            public async Task<string> Handle(PlugRequest request, CancellationToken cancellationToken)
            {
                return string.Empty;
            }
        }
    }
}