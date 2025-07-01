using HotelService.Application.Cqrs.Queries;

namespace HotelService.Application.Features.Booking.Queries.GetAvailableRooms;

public record GetAvailableRoomsQuery(DateTimeOffset From, DateTimeOffset Thru) : IQuery<AvailableRoomsResponse>;