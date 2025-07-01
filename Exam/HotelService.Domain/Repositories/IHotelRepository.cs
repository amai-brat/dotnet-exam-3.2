using HotelService.Domain.Entities;

namespace HotelService.Domain.Repositories;

public interface IHotelRepository
{
    Task<List<Hotel>> GetHotelsWithAvailableRoomsAsync(CancellationToken cancellationToken = default);
}