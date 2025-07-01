using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using HotelService.Domain.Repositories;
using HotelService.Infrastructure.Data;
using HotelService.Infrastructure.Data.Repositories;
using HotelService.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgresBooking"));
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IOutboxRepository, OutboxRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<GraphQLHttpClient>(_ => 
            new GraphQLHttpClient(configuration
                .GetSection("Services")
                .Get<ServicesOptions>()?.Payment 
                                  ?? throw new InvalidOperationException(), 
                new SystemTextJsonSerializer()));
        
        return services;
    }
}