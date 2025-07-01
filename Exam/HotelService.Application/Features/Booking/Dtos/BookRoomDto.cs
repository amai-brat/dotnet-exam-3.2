using Contracts;

namespace HotelService.Application.Features.Booking.Dtos;

public class BookRoomDto
{
    public int RoomId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public required CardInput Card { get; set; }
}