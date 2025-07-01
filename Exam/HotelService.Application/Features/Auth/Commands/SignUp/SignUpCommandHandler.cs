using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using FluentResults;
using HotelService.Application.Cqrs.Commands;
using HotelService.Domain.Entities;
using HotelService.Domain.Repositories;

namespace HotelService.Application.Features.Auth.Commands.SignUp;

public class SignUpCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<SignUpCommand>
{
    public async Task<Result> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var validationResult = Validate(request);
        if (validationResult.IsFailed)
        {
            return validationResult;
        }
        
        var exist = await userRepository.IsUserExistAsync(request.Email, cancellationToken);
        if (exist)
        {
            return Result.Fail($"Email {request.Email} already exists");
        }
        
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password)),
        };
        
        _ = await userRepository.AddAsync(user, cancellationToken);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }

    private static Result Validate(SignUpCommand request)
    {
        try
        {
            _ = new MailAddress(request.Email);
        }
        catch (Exception)
        {
            return Result.Fail("Invalid email address");
        }
        
        return Result.Ok();
    }
}