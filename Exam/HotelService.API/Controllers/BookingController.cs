using HotelService.API.Helpers;
using HotelService.Application.Features.Booking.Commands.BookRoom;
using HotelService.Application.Features.Booking.Dtos;
using HotelService.Application.Features.Booking.Queries.GetAvailableRooms;
using HotelService.Application.Features.Booking.Queries.GetBooking;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.API.Controllers;

[ApiController]
[Route("bookings")]
public class BookingController(
    IMediator mediator) : ControllerBase
{
    [HttpGet("available-rooms")]
    public async Task<IActionResult> GetAvailableRooms(
        [FromQuery] DateTimeOffset from, 
        [FromQuery] DateTimeOffset thru, 
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAvailableRoomsQuery(from, thru), cancellationToken);
        
        return result.IsSuccess 
            ? Ok(result.Value)
            : BadRequest(result.Errors.ToProblemDetails());
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> BookRoom(
        [FromBody] BookRoomDto dto,
        CancellationToken cancellationToken)
    {
        var userId = this.GetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }
        
        var result = await mediator.Send(new BookRoomCommand(
            userId.Value, 
            dto.RoomId, 
            1, 
            dto.StartDate, 
            dto.EndDate,
            dto.Card), cancellationToken);
        
        return result.IsSuccess 
            ? Created($"/bookings/{result.Value.Id}", result.Value)
            : BadRequest(result.Errors.ToProblemDetails());
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetStatus(Guid id, CancellationToken cancellationToken)
    {
        var userId = this.GetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }
        
        var result = await mediator.Send(new GetBookingQuery(userId.Value, id), cancellationToken);
        
        return result.IsSuccess 
            ? Ok(result.Value)
            : BadRequest(result.Errors.ToProblemDetails());
    }
}