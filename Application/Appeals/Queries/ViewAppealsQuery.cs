using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Appeals.Models;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Extensions;
using HotelAutomationApp.Application.Extensions.IQueryable;
using HotelAutomationApp.Domain.Models.Messaging.Appeals;
using HotelAutomationApp.Persistence.Interfaces.Context;
using HotelAutomationApp.Shared.Common.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Appeals.Queries;

public class ViewAppealsQuery : IRequest<PageResponse<AppealDto>>
{
    public ViewAppealsQuery(
        PageRequest? pageRequest,
        string? email,
        string? userName,
        AppealStatus? status,
        bool fullMatching,
        IPeriod? period)
    {
        PageRequest = pageRequest;
        Email = email;
        UserName = userName;
        Status = status;
        FullMatching = fullMatching;
        Period = period;
    }

    public PageRequest? PageRequest { get; }
    public string? Email { get; }
    public string? UserName { get; }
    public AppealStatus? Status { get; }
    public bool FullMatching { get; }
    public IPeriod? Period { get; set; }

    private class Handler : IRequestHandler<ViewAppealsQuery, PageResponse<AppealDto>>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDb, IMapper mapper)
        {
            _applicationDb = applicationDb;
            _mapper = mapper;
        }

        public async Task<PageResponse<AppealDto>> Handle(ViewAppealsQuery request, CancellationToken cancellationToken)
        {
            var appeals = _applicationDb.Appeal
                .Where(q => request.Status == null || q.Status == request.Status)
                .CreatedBetween(request.Period);

            if (request.Email is not null)
            {
                appeals = appeals.Where(appeal => request.FullMatching
                    ? appeal.Email == request.Email
                    : appeal.Email.Contains(request.Email));
            }

            if (request.UserName is not null)
            {
                appeals = appeals.Where(appeal => request.FullMatching
                    ? appeal.UserName == request.UserName
                    : appeal.UserName.Contains(request.UserName));
            }

            return (await appeals
                    .ProjectTo<AppealDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken))
                .LocalizeByCreatedDate()
                .AsPageResponse(request.PageRequest);
        }
    }
}