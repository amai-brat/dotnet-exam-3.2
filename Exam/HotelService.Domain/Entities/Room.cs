namespace HotelService.Domain.Entities;

public class Room
{
    public int Id { get; set; }
    
    public int HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;
    
    public double Area { get; set; }
    public int GuestCapacity { get; set; }
}