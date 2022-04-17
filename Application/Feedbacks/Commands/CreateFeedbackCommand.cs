using HotelAutomationApp.Domain.Models.Messaging;
using HotelAutomationApp.Domain.Models.Messaging.Appeals;
using HotelAutomationApp.Infrastructure.Interfaces.Auth.Services;
using HotelAutomationApp.Infrastructure.Interfaces.EmailServices;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Common;
using MediatR;

namespace HotelAutomationApp.Application.Feedbacks.Commands;

public class CreateFeedbackCommand : IRequest
{
    public CreateFeedbackCommand(string appealId, string title, string body, bool isBodyHtml)
    {
        AppealId = appealId;
        Title = title;
        Body = body;
        IsBodyHtml = isBodyHtml;
    }

    public string AppealId { get;  }
    public string Title { get;  }
    public string Body { get;  }
    public bool IsBodyHtml { get; }

    private class Handler : AsyncRequestHandler<CreateFeedbackCommand>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly ISecurityContext _securityContext;
        private readonly ISmtpClient _smtpClient;

        public Handler(IApplicationDbContext applicationDb, ISecurityContext securityContext, ISmtpClient smtpClient)
        {
            _applicationDb = applicationDb;
            _securityContext = securityContext;
            _smtpClient = smtpClient;
        }

        protected override async Task Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            var appeal = await _applicationDb.Appeal.FindAsync(request.AppealId.YieldObjectArray());

            var feedback = Feedback.New(appeal!.Id, request.Title, request.Body, _securityContext.UserId);

            _applicationDb.Feedback.Add(feedback);
            await _applicationDb.SaveChangesAsync(CancellationToken.None);

            appeal.FeedbackId = feedback.Id;
            appeal.Status = AppealStatus.Answered;
            await _applicationDb.SaveChangesAsync(CancellationToken.None);

            await _smtpClient.SendMail(request.Title, request.Body, new[] {appeal.Email}, request.IsBodyHtml);
        }
    }
}