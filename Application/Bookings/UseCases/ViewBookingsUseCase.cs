using HotelAutomationApp.Application.Bookings.Models;
using HotelAutomationApp.Application.Bookings.Queries;
using HotelAutomationApp.Application.Common;
using HotelAutomationApp.Application.Common.Pagination;
using HotelAutomationApp.Application.Extensions;
using HotelAutomationApp.Domain.Models.Bookings;
using MediatR;

namespace HotelAutomationApp.Application.Bookings.UseCases;

public class ViewBookingsUseCase : UseCase<ViewBookingsRequest, PageResponse<BookingDto>>
{
    private readonly IMediator _mediator;

    public ViewBookingsUseCase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<PageResponse<BookingDto>> HandleAsync(ViewBookingsRequest request, CancellationToken cancellationToken)
    {
        var query = new ViewBookingsQuery(
            request.ClientName,
            request.ClientEmail,
            request.RoomName,
            request.BookingState,
            request.MinTotalPrice,
            request.MaxTotalPrice,
            request.DateFrom,
            request.DateTo,
            request.RoomDescription,
            request.RoomGroupName,
            request.RoomGroupDescription);

        return (await _mediator.Send(query, cancellationToken)).AsPageResponse(request.PageRequest);
    }
}

public class ViewBookingsRequest : IRequest<PageResponse<BookingDto>>
{
    public PageRequest? PageRequest { get; set; }
    public string? ClientName { get; set; }
    public string? ClientEmail { get; set; }
    public string? RoomName { get; set; }
    public BookingState? BookingState { get; set; }
    public decimal? MinTotalPrice { get; set; }
    public decimal? MaxTotalPrice { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string? RoomDescription { get; set; }
    public string? RoomGroupName { get; set; }
    public string? RoomGroupDescription { get; set; }
}