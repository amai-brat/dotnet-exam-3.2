using FluentResults;
using MediatR;

namespace HotelService.Application.Cqrs.Queries;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;