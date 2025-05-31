using LibPoint.Application.Features.Books.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly CreateBookCommandHandler createBookCommandHandler;
        private readonly GetBookByIdQueryHandler getBookByIdQueryHandler;
        private readonly GetBookQueryHandler getBookQueryHandler;
        private readonly UpdateBookCommandHandler updateBookCommandHandler;
        private readonly RemoveBookCommandHandler removeBookCommandHandler;

        public BookController(CreateBookCommandHandler createBookCommandHandler, GetBookByIdQueryHandler getBookByIdQueryHandler, GetBookQueryHandler getBookQueryHandler, UpdateBookCommandHandler updateBookCommandHandler, RemoveBookCommandHandler removeBookCommandHandler)
        {
            this.createBookCommandHandler = createBookCommandHandler;
            this.getBookByIdQueryHandler = getBookByIdQueryHandler;
            this.getBookQueryHandler = getBookQueryHandler;
            this.updateBookCommandHandler = updateBookCommandHandler;
            this.removeBookCommandHandler = removeBookCommandHandler;
        }

        [HttpGet("BookList")]
        public async Task<IActionResult> BookList()
        {
            var values = await getBookQueryHandler.Handle();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var value = await getBookByIdQueryHandler.Handle(new Application.Features.Books.Queries.GetBookByIdQuery(id));
            return Ok(value);
        }

        [HttpGet("RemoveBook")]
        public async Task<IActionResult> RemoveBook(Guid id)
        {
            await removeBookCommandHandler.Handle(new Application.Features.Books.Commands.RemoveBookCommand(id));
            return Ok("Book has been deleted succesfully");
        }
    }
}
