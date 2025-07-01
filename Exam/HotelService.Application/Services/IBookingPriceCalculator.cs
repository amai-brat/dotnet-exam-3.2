using HotelService.Domain.Entities;

namespace HotelService.Application.Services;

public interface IBookingPriceCalculator
{
    decimal GetPrice(Room room, Currency currency, DateTimeOffset from, DateTimeOffset thru);
}