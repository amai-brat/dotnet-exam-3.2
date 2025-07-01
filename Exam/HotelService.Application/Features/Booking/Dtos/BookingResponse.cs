using HotelService.Domain.Entities;

namespace HotelService.Application.Features.Booking.Dtos;

public class BookingResponse
{
    public Guid Id { get; set; }
    public BookingStatus Status { get; set; }
    public int RoomId { get; set; }
    public int UserId { get; set; }
    public int CurrencyId { get; set; }
    
    public decimal Price { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public static BookingResponse MapFrom(Domain.Entities.Booking booking)
    {
        return new BookingResponse
        {
            Id = booking.Id,
            Status = booking.Status,
            RoomId = booking.RoomId,
            UserId = booking.UserId,
            CurrencyId = booking.CurrencyId,
            Price = booking.Price,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate
        };
    }
}