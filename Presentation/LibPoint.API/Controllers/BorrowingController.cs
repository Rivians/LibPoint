using LibPoint.Application.Features.Borrowings.Commands;
using LibPoint.Application.Features.Borrowings.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BorrowingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllBorrowings")]
        public async Task<IActionResult> GetAllBorrowings()
        {
            var response = await _mediator.Send(new GetAllBorrowingsQueryRequest());
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetBorrowingById")]
        public async Task<IActionResult> GetBorrowingById([FromQuery] Guid id)
        {
            var response = await _mediator.Send(new GetBorrowingByIdQueryRequest(id));
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("GetBorrowingsByUserId")]
        public async Task<IActionResult> GetBorrowingsByUserId([FromQuery] Guid userId)
        {
            var response = await _mediator.Send(new GetBorrowingByUserIdQueryRequest(userId));
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("GetBorrowingsByBookId")]
        public async Task<IActionResult> GetBorrowingsByBookId([FromQuery] Guid bookId)
        {
            var response = await _mediator.Send(new GetBorrowingsByBookIdQueryRequest(bookId));
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPost("CreateBorrowing")]
        public async Task<IActionResult> CreateBorrowing([FromBody] CreateBorrowingCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPut("UpdateBorrowing")]
        public async Task<IActionResult> UpdateBorrowing([FromBody] UpdateBorrowingCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpDelete("DeleteBorrowing")]
        public async Task<IActionResult> DeleteBorrowing([FromQuery] Guid id)
        {
            var response = await _mediator.Send(new DeleteBorrowingCommandRequest(id));
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
