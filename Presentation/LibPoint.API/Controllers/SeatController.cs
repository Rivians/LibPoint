using LibPoint.Application.Features.Seats.Queries;
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
    }
}
