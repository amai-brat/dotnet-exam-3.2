using HotelService.Application.Features.Booking.Dtos;
using HotelService.Domain.Entities;

namespace HotelService.Application.Mappers;

public static class HotelMapper
{
    public static HotelDto ToDto(this Hotel hotel, List<RoomDto> rooms)
    {
        return new HotelDto
        {
            Id = hotel.Id,
            Name = hotel.Name,
            Address = hotel.Address,
            Stars = hotel.Stars,
        };
    }

    public static RoomDto ToDto(this Room room, decimal price)
    {
        return new RoomDto
        {
            Id = room.Id,
            HotelId = room.HotelId,
            Area = room.Area,
            GuestCapacity = room.GuestCapacity,
            IsBooked = room.IsBooked,
            Price = price
        };
    }
}