namespace HotelService.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; }
    
    public int RoomId { get; set; }
    public required Room Room { get; set; }

    public int UserId { get; set; }
    public required User User { get; set; }

    public int CurrencyId { get; set; }
    public required Currency Currency { get; set; }
    public decimal Price { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}