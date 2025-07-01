using Contracts;
using GraphQL;
using GraphQL.Client.Http;
using HotelService.Application.Features.Booking.Commands.Purchase;
using HotelService.Application.Helpers;
using HotelService.Domain.Repositories;
using MediatR;

namespace HotelService.API.Services;

public class OutboxWorker(
    ILogger<OutboxWorker> logger,
    IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            
            await using var scope = scopeFactory.CreateAsyncScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var outboxRepository = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            
            var toSend = await outboxRepository.GetNotSentMessagesAsync(stoppingToken);

            foreach (var message in toSend)
            {
                try
                {
                    var @event = message.ToObject();

                    if (@event is PurchaseInput purchase)
                    {
                        var result = await mediator.Send(new PurchaseCommand(purchase), stoppingToken);
                        if (result.IsSuccess)
                        {
                            message.IsSent = true;
                            message.SentDate = DateTime.UtcNow;
                        }
                        else
                        {
                            throw new Exception("Something went wrong");
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                    await unitOfWork.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error while sending message: {Exception}", ex.Message);
                }
            }
            
            await Task.Delay(5000, stoppingToken);
        }
    }
}