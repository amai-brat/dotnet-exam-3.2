using System.Security.Cryptography;
using System.Text;
using FluentResults;
using HotelService.Application.Cqrs.Commands;
using HotelService.Application.Services;
using HotelService.Domain.Repositories;

namespace HotelService.Application.Features.Auth.Commands.SignIn;

public class SignInCommandHandler(
    IUserRepository userRepository,
    ITokenService tokenService) : ICommandHandler<SignInCommand, TokenDto>
{
    public async Task<Result<TokenDto>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user is null)
        {
            return Result.Fail("User not found");
        }
        
        if (!Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password))
            .SequenceEqual(user.PasswordHash))
        {
            return Result.Fail("Invalid password");
        }
        
        var token = tokenService.GenerateToken(user);
        return Result.Ok(new TokenDto
        {
            AccessToken = token
        });
    }
}