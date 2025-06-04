using LibPoint.Application.Features.Authors.Commands;
using LibPoint.Application.Features.Authors.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibPoint.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController: ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("create-author")]
    public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut("update-author")]
    public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("remove-author")]
    public async Task<IActionResult> RemoveAuthor([FromQuery] Guid id)
    {
        var response = await _mediator.Send(new RemoveAuthorCommandRequest(id));
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-author-by-id")]
    public async Task<IActionResult> GetAuthorById([FromQuery] Guid id)
    {
        var response = await _mediator.Send(new GetAuthorByIdQueryRequest(id));
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet("get-all-authors")]
    public async Task<IActionResult> GetAllAuthors()
    {
        var response = await _mediator.Send(new GetAllAuthorsQueryRequest());
        return response.Success ? Ok(response) : BadRequest(response);
    }
    
}