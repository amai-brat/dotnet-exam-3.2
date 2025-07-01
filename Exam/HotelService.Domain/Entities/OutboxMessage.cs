namespace HotelService.Domain.Entities;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public string EventType { get; set; } = null!;
    public string EventPayload { get; set; } = null!;
    public DateTime EventDate { get; set; } = DateTime.UtcNow;
    public bool IsSent { get; set; }
    public DateTime? SentDate { get; set; }
}