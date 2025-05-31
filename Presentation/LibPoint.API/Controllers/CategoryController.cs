using LibPoint.Application.Features.Categories.Queries;
using LibPoint.Application.Features.Categories.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("CategoryList")]
        public async Task<IActionResult> GetCategoryList()
        {
            var values = await _mediator.Send(new GetAllCategoriesQueryRequest());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var value = await _mediator.Send(new GetCategoryByIdQueryRequest(id));
            return Ok(value);
        }

        [HttpDelete("RemoveCategory")]
        public async Task<IActionResult> RemoveCategory(RemoveCategoryCommandRequest request)
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

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommandRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Could not create a category");
            }
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommandRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Could not update the category");
            }
        }
    }
}
