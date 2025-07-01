using System.Text.Json;
using Contracts;
using HotelService.Domain.Entities;

namespace HotelService.Application.Helpers;

public static class OutboxMessageExtensions
{
    public static OutboxMessage ToOutboxMessage(this OutboxedEvent @event)
    {
        var type = @event.GetType();
        return new OutboxMessage
        {
            EventType = type.AssemblyQualifiedName!,
            EventPayload = JsonSerializer.Serialize(@event, type),
        };
    }

    public static object ToObject(this OutboxMessage message)
    {
        return JsonSerializer.Deserialize(
                   message.EventPayload, 
                   Type.GetType(message.EventType) ?? throw new InvalidOperationException("Couldn't resolve type of Event")) 
               ?? throw new InvalidOperationException("Couldn't deserialize an Event");
    }
}