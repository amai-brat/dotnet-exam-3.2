using HotelService.Application.Cqrs.Queries;
using HotelService.Application.Features.Booking.Dtos;

namespace HotelService.Application.Features.Booking.Queries.GetBooking;

public record GetBookingQuery(int UserId, Guid BookingId) : IQuery<BookingResponse>;