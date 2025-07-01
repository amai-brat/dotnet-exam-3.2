namespace HotelService.Domain.Entities;

public enum BookingStatus
{
    None = 0,
    Processing = 1,
    Success = 2,
    Failed = 3
}

public class Booking
{
    public Guid Id { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.None;
    
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