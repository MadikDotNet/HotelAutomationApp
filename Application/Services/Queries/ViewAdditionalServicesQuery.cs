using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Extensions;
using HotelAutomationApp.Application.Extensions.IQueryable;
using HotelAutomationApp.Application.Services.Models;
using HotelAutomationApp.Persistence.Interfaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelAutomationApp.Application.Services.Queries;

public class ViewAdditionalServicesQuery : IRequest<PageResponse<ServiceDto>>
{
    public ViewAdditionalServicesQuery(
        PageRequest? pageRequest,
        string? code,
        string? name,
        string? description,
        bool fullMatching)
    {
        PageRequest = pageRequest;
        Code = code;
        Name = name;
        Description = description;
        FullMatching = fullMatching;
    }

    public PageRequest? PageRequest { get; }
    public string? Code { get; }
    public string? Name { get; }
    public string? Description { get; }
    public bool FullMatching { get; }

    private class Handler : IRequestHandler<ViewAdditionalServicesQuery, PageResponse<ServiceDto>>
    {
        private readonly IApplicationDbContext _applicationDb;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDb, IMapper mapper)
        {
            _applicationDb = applicationDb;
            _mapper = mapper;
        }

        public async Task<PageResponse<ServiceDto>> Handle(ViewAdditionalServicesQuery request,
            CancellationToken cancellationToken)
        {
            return (await _applicationDb.Service.Where(q => q.IsAdditional)
                .Filter(request.Code, request.Name, request.Description, request.FullMatching)
                .ProjectTo<ServiceDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken))
                .AsPageResponse(request.PageRequest);
        }
    }
}