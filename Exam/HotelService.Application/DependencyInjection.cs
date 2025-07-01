using System.Reflection;
using HotelService.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HotelService.Application;

public static class DependencyInjection
{
    private static readonly Assembly Assembly = typeof(DependencyInjection).Assembly;
    
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(Assembly);
        });
        
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IBookingPriceCalculator, BookingPriceCalculcator>();
        
        return services;
    }
}