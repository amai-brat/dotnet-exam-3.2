namespace Contracts;

public record PurchaseInput(Guid BookingId, int UserId, string CurrencyCode, decimal Amount, string Reason, CardInput Card) : OutboxedEvent;
public record CardInput(string CardNumber, string CardOwner, string ValidThru, int Cvc);

public record PurchaseResponse(string TransactionId, TransactionStatus Status);

public enum TransactionStatus
{
    Pending = 0,
    Completed = 1,
    Failed = 2,
    Rejected = 3,
    Refaunded = 10
}
