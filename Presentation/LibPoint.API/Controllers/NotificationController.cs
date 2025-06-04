using LibPoint.Application.Features.Notifications.Commands;
using LibPoint.Application.Features.Notifications.Queries;
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
            var result = await _mediator.Send(new GetNotificationsByUserIdQueryRequest(userId));

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost("create-notification")]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNofiticationCommandRequest command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
