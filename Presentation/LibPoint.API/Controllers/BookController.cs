using LibPoint.Application.Features.Books.Commands;
using LibPoint.Application.Features.Books.Handlers;
using LibPoint.Application.Features.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("BookList")]
        public async Task<IActionResult> GetBookList()
        {
            var values = await _mediator.Send(new GetBooksQueryRequest());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var value = await _mediator.Send(new GetBookByIdQueryRequest(id));
            return Ok(value);
        }

        [HttpDelete("RemoveBook")]
        public async Task<IActionResult> RemoveBook(RemoveBookCommandRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook(CreateBookCommandRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("could not create a book");
            }
        }

        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBook(UpdateBookCommandRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
