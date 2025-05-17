using LibPoint.Application.Features.Seats.Commands;
using LibPoint.Application.Features.Seats.Queries;
using LibPoint.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SeatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-seat-list")]
        public async Task<IActionResult> GetSeatList()
        {
            var response = await _mediator.Send(new GetAllSeatsQueryRequest());

            if (response.Success is false)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("set-seat-free")]
        public async Task<IActionResult> SetSeatFree([FromHeader] Guid seatId)
        {
            if (seatId == Guid.Empty)
                return BadRequest(new ResponseModel<bool>("Invalid seatId."));

            var result = await _mediator.Send(new SetSeatFreeCommandRequest(seatId));
            if (result.Success)
                return Ok(result);
            else
            {
                return BadRequest(result);
            }                  
        }

        [HttpPost("set-seat-reserved")]
        public async Task<IActionResult> SetSeatReserved([FromHeader] Guid appUserId, [FromHeader] Guid seatId, [FromHeader] Guid reservationId)
        {
            if (appUserId == Guid.Empty || seatId == Guid.Empty)
                return BadRequest(new ResponseModel<bool>("AppUserId or SeatId is invalid"));

            var result = await _mediator.Send(new SetSeatReservedCommandRequest(appUserId , seatId, reservationId));
            if (result.Success)
                return Ok(result);
            else
            {
                return BadRequest(result);
            }
        }
    }
}
