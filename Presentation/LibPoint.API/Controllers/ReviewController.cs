using LibPoint.Application.Features.Reviews.Commands;
using LibPoint.Application.Features.Reviews.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibPoint.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController: ControllerBase
{
     private readonly IMediator _mediator;

    public ReviewController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create-review")]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut("update-review")]
    public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("delete-review")]
    public async Task<IActionResult> DeleteReview([FromQuery] Guid id)
    {
        var response = await _mediator.Send(new DeleteReviewCommandRequest(id));
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-review-by-id")]
    public async Task<IActionResult> GetReviewById([FromQuery] Guid id)
    {
        var response = await _mediator.Send(new GetReviewByIdQueryRequest(id));
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet("get-all-reviews")]
    public async Task<IActionResult> GetAllReviews()
    {
        var response = await _mediator.Send(new GetAllReviewsQueryRequest());
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("get-reviews-by-book-id")]
    public async Task<IActionResult> GetReviewsByBookId([FromQuery] Guid bookId)
    {
        var response = await _mediator.Send(new GetReviewsByBookIdQueryRequest(bookId));
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet("get-reviews-by-user-id")]
    public async Task<IActionResult> GetReviewsByUserId([FromQuery] Guid userId)
    {
        var response = await _mediator.Send(new GetReviewsByUserIdQueryRequest(userId));
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet("get-average-rating-by-book-id")]
    public async Task<IActionResult> GetAverageRating([FromQuery] Guid bookId)
    {
        var response = await _mediator.Send(new GetAverageRatingByBookQueryRequest(bookId));
        return response.Success ? Ok(response) : NotFound(response);
    }
}