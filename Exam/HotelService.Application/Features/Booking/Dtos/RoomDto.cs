namespace HotelService.Application.Features.Booking.Dtos;

public class RoomDto
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public double Area { get; set; }
    public int GuestCapacity { get; set; }
    public bool IsBooked { get; set; }
    public decimal Price { get; set; }
}