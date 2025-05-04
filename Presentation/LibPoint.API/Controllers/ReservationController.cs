using LibPoint.Application.Features.Reservations.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommandRequest request, CancellationToken cancellationToken)
        {
            var responseModel = await _mediator.Send(request);

            if (responseModel.Success)
                return Ok(responseModel);
            else
                return BadRequest(responseModel);
        }
    }
}
