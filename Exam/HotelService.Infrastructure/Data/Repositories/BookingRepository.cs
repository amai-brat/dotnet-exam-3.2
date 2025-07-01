using HotelService.Domain.Entities;
using HotelService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Data.Repositories;

public class BookingRepository(AppDbContext dbContext) : IBookingRepository
{
    public async Task<Booking> AddAsync(Booking booking, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.Bookings.AddAsync(booking, cancellationToken: cancellationToken);
        return entry.Entity;
    }

    public async Task<Booking?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }
}