using HotelService.Domain.Entities;
using HotelService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Data.Repositories;

public class OutboxRepository(AppDbContext dbContext) : IOutboxRepository
{
    public async Task<OutboxMessage> AddAsync(OutboxMessage outboxMessage, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.OutboxMessages.AddAsync(outboxMessage, cancellationToken);
        return entry.Entity;
    }

    public async Task<List<OutboxMessage>> GetNotSentMessagesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.OutboxMessages
            .Where(x => !x.IsSent)
            .ToListAsync(cancellationToken);
    }
}