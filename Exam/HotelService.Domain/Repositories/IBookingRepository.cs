using HotelService.Domain.Entities;

namespace HotelService.Domain.Repositories;

public interface IBookingRepository
{
    Task<Booking> AddAsync(Booking booking, CancellationToken cancellationToken = default);
    Task<Booking?> GetById(Guid id, CancellationToken cancellationToken = default);
}