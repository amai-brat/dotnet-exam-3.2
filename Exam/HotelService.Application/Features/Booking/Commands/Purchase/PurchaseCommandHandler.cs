using Contracts;
using FluentResults;
using GraphQL;
using GraphQL.Client.Http;
using HotelService.Application.Cqrs.Commands;
using HotelService.Domain.Entities;
using HotelService.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace HotelService.Application.Features.Booking.Commands.Purchase;

public class PurchaseCommandHandler(
    ILogger<PurchaseCommandHandler> logger,
    GraphQLHttpClient client,
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<PurchaseCommand>
{
    private class ResponseType
    {
        public PurchaseResponse Purchase { get; set; } = null!;
    }
    
    private const string PaymentQuery = """
                                           mutation Purchase($input: PurchaseInput!) {
                                               purchase(input: $input) {
                                                   transactionId
                                                   status
                                               }
                                           }
                                           """;
    
    public async Task<Result> Handle(PurchaseCommand request, CancellationToken cancellationToken)
    {
        var booking = await bookingRepository.GetById(request.Input.BookingId, cancellationToken);
        if (booking == null)
        {
            return Result.Fail("Booking not found");
        }
        
        var purchase = request.Input;
        var paymentRequest = new GraphQLRequest
        {
            Query = PaymentQuery,
            Variables = new
            {
                input = new
                {
                    amount = purchase.Amount,
                    bookingId = purchase.BookingId,
                    currencyCode = purchase.CurrencyCode,
                    reason = purchase.Reason,
                    userId = purchase.UserId,
                    card = new
                    {
                        cardNumber = purchase.Card.CardNumber,
                        cardOwner = purchase.Card.CardOwner,
                        validThru = purchase.Card.ValidThru,
                        cvc = purchase.Card.Cvc
                    }
                }
            }
        };

        try
        {
            var response = await client.SendQueryAsync<ResponseType>(paymentRequest, cancellationToken);
            if (response.Data.Purchase.Status == TransactionStatus.Completed)
            {
                booking.Room.IsBooked = true;
                booking.Status = BookingStatus.Success;
            }
            else
            {
                booking.Status = BookingStatus.Failed;
            }
        }
        catch (Exception e)
        {
            logger.LogError("Error in GraphQL request: {Error}", e.Message);
            return Result.Fail("Error in GraphQL request");
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }
}