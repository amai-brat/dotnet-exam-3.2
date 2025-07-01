using HotelService.Application.Cqrs.Commands;

namespace HotelService.Application.Features.Auth.Commands.SignUp;

public record SignUpCommand(string Name, string Email, string Password) : ICommand;