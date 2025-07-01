using HotelService.Domain.Entities;

namespace HotelService.Domain.Repositories;

public interface ICurrencyRepository
{
    Task<Currency?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}