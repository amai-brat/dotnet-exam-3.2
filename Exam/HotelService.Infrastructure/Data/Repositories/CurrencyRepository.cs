using HotelService.Domain.Entities;
using HotelService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Data.Repositories;

public class CurrencyRepository(AppDbContext dbContext) : ICurrencyRepository
{
    public async Task<Currency?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var currency = await dbContext.Currencies.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        return currency;
    }
}