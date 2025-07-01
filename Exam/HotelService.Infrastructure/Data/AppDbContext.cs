using HotelService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Currency> Currencies => Set<Currency>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        Seed(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>().HasData(new Currency
        {
            Id = 1,
            Name = "RUB"
        });

        List<Room> rooms =
        [
            new()
            {
                Id = 1,
                HotelId = 1,
                Area = 50,
                GuestCapacity = 4
            },
            new()
            {
                Id = 2,
                HotelId = 1,
                Area = 25,
                GuestCapacity = 2
            },
            new()
            {
                Id = 3,
                HotelId = 1,
                Area = 35,
                GuestCapacity = 3
            }
        ];
        
        modelBuilder.Entity<Room>().HasData(rooms);
        modelBuilder.Entity<Hotel>().HasData(new Hotel
        {
            Id = 1,
            Name = "Trivago",
            Stars = 5,
            Address = "Казань"
        });
    }
}