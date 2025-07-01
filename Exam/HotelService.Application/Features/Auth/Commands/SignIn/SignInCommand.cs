using HotelService.Application.Cqrs.Commands;

namespace HotelService.Application.Features.Auth.Commands.SignIn;

public record SignInCommand(string Email, string Password) : ICommand<TokenDto>;