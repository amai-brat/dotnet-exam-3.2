using HotelService.Domain.Entities;

namespace HotelService.Application.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}