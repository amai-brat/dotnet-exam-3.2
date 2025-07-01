using Contracts;
using HotelService.Application.Cqrs.Commands;

namespace HotelService.Application.Features.Booking.Commands.Purchase;

public record PurchaseCommand(PurchaseInput Input) : ICommand;