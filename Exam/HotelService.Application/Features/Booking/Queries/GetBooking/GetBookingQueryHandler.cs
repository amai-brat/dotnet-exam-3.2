using FluentResults;
using HotelService.Application.Cqrs.Queries;
using HotelService.Application.Features.Booking.Dtos;
using HotelService.Domain.Repositories;

namespace HotelService.Application.Features.Booking.Queries.GetBooking;

public class GetBookingQueryHandler(
    IBookingRepository bookingRepository) : IQueryHandler<GetBookingQuery, BookingResponse>
{
    public async Task<Result<BookingResponse>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        var booking = await bookingRepository.GetById(request.BookingId, cancellationToken);
        if (booking == null)
        {
            return Result.Fail("Booking not found");
        }
        
        return BookingResponse.MapFrom(booking);
    }
}