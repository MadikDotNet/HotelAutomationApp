using HotelAutomationApp.Domain.Models.Messaging.Appeals;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;

namespace HotelAutomationApp.Application.Appeals.Commands;

public class CreateAppealCommand : IRequest
{
    public CreateAppealCommand(string email, string title, string body, string userName)
    {
        Email = email;
        Title = title;
        Body = body;
        UserName = userName;
    }

    public string Email { get; }
    public string Title { get; }
    public string Body { get; }
    public string UserName { get; }
    
    private class Handler : AsyncRequestHandler<CreateAppealCommand>
    {
        private readonly IApplicationDbContext _applicationDb;

        public Handler(IApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }

        protected override async Task Handle(CreateAppealCommand request, CancellationToken cancellationToken)
        {
            var appeal = Appeal.New(request.Email, request.UserName, request.Title, request.Body);

            _applicationDb.Appeal.Add(appeal);
            await _applicationDb.SaveChangesAsync(cancellationToken);
        }
    }
}