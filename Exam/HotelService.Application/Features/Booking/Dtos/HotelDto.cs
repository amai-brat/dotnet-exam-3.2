namespace HotelService.Application.Features.Booking.Dtos;

public class HotelDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Stars { get; set; }
    public required string Address { get; set; }
    public List<RoomDto> Rooms { get; set; } = [];
}