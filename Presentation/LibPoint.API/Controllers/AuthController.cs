using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Notifications.Commands;
using LibPoint.Application.Features.User.Commands;
using LibPoint.Application.Features.User.Queries;
using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;
        public AuthController(ITokenService tokenService, IMediator mediator)
        {
            _tokenService = tokenService;
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommandRequest loginCommandRequest)
        {
            ResponseModel<UserLoginModel> responseModel = await _mediator.Send(loginCommandRequest);

            if (responseModel.Success is false)
                return BadRequest(responseModel);

            return Ok(responseModel);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommandRequest registerCommandRequest)
        {
            ResponseModel<UserRegisterModel> responseModel = await _mediator.Send(registerCommandRequest);

            if (responseModel.Success is false)
                return BadRequest(responseModel);

            return Ok(responseModel);
        }

        [HttpGet("get-user-list")]
        public async Task<IActionResult> GetUserList()
        {
            var command = new GetUserListQueryRequest();
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete-user-by-id")]        
        public async Task<IActionResult> DeleteUserById([FromHeader] Guid userId)
        {
            ResponseModel<bool> responseModel = await _mediator.Send(new DeleteUserByIdCommandRequest(userId));

            if (!responseModel.Success)
                return BadRequest(responseModel);

            return Ok(responseModel);
        }
    }
}
