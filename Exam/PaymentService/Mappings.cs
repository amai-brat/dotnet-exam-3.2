using Contracts;
using PaymentService.Models;

namespace PaymentService;

public static class Mappings
{
    public static Card ToCardEntity(this CardInput card)
    {
        return new Card
        {
            CardNumber = card.CardNumber,
            CardOwner = card.CardOwner,
            ValidThru = card.ValidThru,
            Cvc = card.Cvc,
        };
    }
}