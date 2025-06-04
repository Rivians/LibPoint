using LibPoint.Application.Features.Notifications.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-notifications-by-userId")]
        public async Task<IActionResult> GetNotificationsByUserId(Guid userId)
        {

        }

        [HttpPost("create-notification")]
        public async Task<IActionResult> CraeteNotification([FromBody] CreateNofiticationCommandRequest command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
