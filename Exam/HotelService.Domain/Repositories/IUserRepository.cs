using HotelService.Domain.Entities;

namespace HotelService.Domain.Repositories;

public interface IUserRepository
{
    Task<bool> IsUserExistAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<User> AddAsync(User user, CancellationToken cancellationToken = default);
}