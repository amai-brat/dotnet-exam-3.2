using HotelService.Domain.Entities;

namespace HotelService.Application.Services;

public class BookingPriceCalculcator : IBookingPriceCalculator
{
    public decimal GetPrice(Room room, Currency currency, DateTimeOffset from, DateTimeOffset thru)
    {
        return (decimal)(room.GuestCapacity * room.Area) + 300m;
    }
}