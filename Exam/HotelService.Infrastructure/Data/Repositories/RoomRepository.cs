using HotelService.Domain.Entities;
using HotelService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Data.Repositories;

public class RoomRepository(AppDbContext dbContext) : IRoomRepository
{
    public async Task<Room?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var room = await dbContext.Rooms.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        return room;
    }
}