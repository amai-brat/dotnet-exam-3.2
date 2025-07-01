using HotelService.Domain.Entities;

namespace HotelService.Domain.Repositories;

public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(int id, CancellationToken cancellationToken);
}