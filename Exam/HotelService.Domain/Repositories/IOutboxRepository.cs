using HotelService.Domain.Entities;

namespace HotelService.Domain.Repositories;

public interface IOutboxRepository
{
    Task<OutboxMessage> AddAsync(OutboxMessage outboxMessage, CancellationToken cancellationToken = default);
    Task<List<OutboxMessage>> GetNotSentMessagesAsync(CancellationToken cancellationToken = default);
}