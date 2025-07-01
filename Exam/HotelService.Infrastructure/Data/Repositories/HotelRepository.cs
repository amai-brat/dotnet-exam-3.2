using HotelService.Domain.Entities;
using HotelService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Data.Repositories;

public class HotelRepository(AppDbContext dbContext) : IHotelRepository
{
    public async Task<List<Hotel>> GetHotelsWithAvailableRoomsAsync(CancellationToken cancellationToken = default)
    {
        var hotels = await dbContext.Hotels
            .Include(x => x.Rooms.Where(r => !r.IsBooked))
            .ToListAsync(cancellationToken: cancellationToken);
        return hotels;
    }
}