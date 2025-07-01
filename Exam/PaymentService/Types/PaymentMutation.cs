using Contracts;
using HotChocolate.Language;
using PaymentService.Data.Abstractions;
using PaymentService.Exceptions;
using PaymentService.Models;
using PaymentService.Services;
using TransactionStatus = PaymentService.Models.TransactionStatus;

namespace PaymentService.Types;
public record PurchasePayload(string TransactionId, TransactionStatus Status);

[ExtendObjectType(OperationType.Mutation)]
public class PaymentMutation
{
    public async Task<PurchasePayload> Purchase(
        PurchaseInput input,
        [Service] ILogger<PaymentMutation> logger,
        [Service] IAccountProvider accountProvider,
        [Service] ITransactionRepository transactionRepository,
        [Service] ICurrencyRepository currencyRepository,
        [Service] IUnitOfWork unitOfWork)
    {
        try
        {
            var card = input.Card.ToCardEntity();

            var currency = await currencyRepository.GetCurrencyAsync(input.CurrencyCode);
            if (currency == null)
            {
                throw new NotFoundException("Currency not found");
            }

            var accNumberFrom = await accountProvider.GetAccountNumber(card);
            if (accNumberFrom == null)
            {
                throw new NotFoundException("Account number not found");
            }

            var accNumberTo = await accountProvider.GetOrganizationAccountNumber();

            var transaction = Transaction.Create(
                input.UserId,
                accNumberFrom,
                accNumberTo,
                GetReasonType(input.Reason),
                currency,
                input.Amount,
                status: TransactionStatus.Completed
            );

            transaction = await transactionRepository.AddTransaction(transaction);
            await unitOfWork.SaveChangesAsync();

            return new PurchasePayload(transaction.Id.ToString(), transaction.Status);
        }
        catch (Exception ex)
        {
            logger.LogWarning("During payment process error occured: {Message}", ex.Message);
            throw;
        }
    }
    
    private static ReasonType GetReasonType(string reason)
    {
        return reason.Contains("booking", StringComparison.CurrentCultureIgnoreCase)
            ? ReasonType.Booking
            : ReasonType.Other;
    }
}