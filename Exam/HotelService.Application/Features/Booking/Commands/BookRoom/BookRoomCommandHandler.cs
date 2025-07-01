using Contracts;
using FluentResults;
using HotelService.Application.Cqrs.Commands;
using HotelService.Application.Helpers;
using HotelService.Application.Services;
using HotelService.Domain.Entities;
using HotelService.Domain.Repositories;

namespace HotelService.Application.Features.Booking.Commands.BookRoom;

public class BookRoomCommandHandler(
    IBookingPriceCalculator priceCalculator,
    ICurrencyRepository currencyRepository,
    IUserRepository userRepository,
    IRoomRepository roomRepository,
    IBookingRepository bookingRepository,
    IOutboxRepository outboxRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<BookRoomCommand>
{
    public async Task<Result> Handle(BookRoomCommand request, CancellationToken cancellationToken)
    {
        var currency = await currencyRepository.GetByIdAsync(request.CurrencyId, cancellationToken);
        if (currency == null)
        {
            return Result.Fail("Currency not found");
        }
        
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            return Result.Fail("User not found");
        }
        
        var room = await roomRepository.GetByIdAsync(request.RoomId, cancellationToken);
        if (room == null)
        {
            return Result.Fail("Room not found");
        }
        
        var booking = new Domain.Entities.Booking
        {
            Id = Guid.CreateVersion7(),
            Status = BookingStatus.Processing,
            Room = room,
            User = user,
            Currency = currency,
            Price = priceCalculator.GetPrice(room, currency, request.StartDate, request.EndDate),
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        var @event = new PurchaseInput(booking.Id, user.Id, currency.Name, booking.Price, "booking", request.Card);
        await outboxRepository.AddAsync(@event.ToOutboxMessage(), cancellationToken);
        
        await bookingRepository.AddAsync(booking, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }
}