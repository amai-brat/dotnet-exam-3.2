using FluentResults;
using HotelService.Application.Cqrs.Queries;
using HotelService.Application.Features.Booking.Dtos;
using HotelService.Application.Mappers;
using HotelService.Application.Services;
using HotelService.Domain.Entities;
using HotelService.Domain.Repositories;

namespace HotelService.Application.Features.Booking.Queries.GetAvailableRooms;

public class GetAvailableRoomsQueryHandler(
    ICurrencyRepository currencyRepository,
    IBookingPriceCalculator priceCalculator,
    IHotelRepository hotelRepository) : IQueryHandler<GetAvailableRoomsQuery, AvailableRoomsResponse>
{
    public async Task<Result<AvailableRoomsResponse>> Handle(GetAvailableRoomsQuery request, CancellationToken cancellationToken)
    {
        // hardcoded
        var currency = await currencyRepository.GetByIdAsync(1, cancellationToken);
        if (currency == null)
        {
            return Result.Fail("Currency not found");
        }
        
        var hotels = await hotelRepository.GetHotelsWithAvailableRoomsAsync(cancellationToken);
        
        var result = new List<HotelDto>();
        foreach (var hotel in hotels)
        {
            var rooms = hotel.Rooms
                .Select(room => room.ToDto(price: priceCalculator.GetPrice(room, currency, request.From, request.Thru)))
                .ToList();

            result.Add(hotel.ToDto(rooms));
        }
        return new AvailableRoomsResponse
        {
            Hotels = result
        };
    }
}