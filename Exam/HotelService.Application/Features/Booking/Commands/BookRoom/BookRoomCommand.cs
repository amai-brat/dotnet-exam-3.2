using Contracts;
using HotelService.Application.Cqrs.Commands;

namespace HotelService.Application.Features.Booking.Commands.BookRoom;

public record BookRoomCommand(
    int UserId, 
    int RoomId, 
    int CurrencyId, 
    DateTime StartDate, 
    DateTime EndDate, 
    CardInput Card) : ICommand;