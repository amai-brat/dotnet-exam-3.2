using HotelService.Application.Features.Booking.Dtos;

namespace HotelService.Application.Features.Booking.Queries.GetAvailableRooms;

public class AvailableRoomsResponse
{
    public List<HotelDto> Hotels { get; set; } = [];
}