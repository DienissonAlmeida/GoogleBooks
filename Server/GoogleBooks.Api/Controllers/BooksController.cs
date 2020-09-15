using GoogleBooks.Domain.Domain;
using GoogleBooks.Domain.Dtos;
using GoogleBooks.Domain.Dtos.Output;
using GoogleBooks.Domain.Dtos.Output.Exceptions;
using GoogleBooks.Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GoogleBooks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly IDomainFactory _domainFactory;
        private readonly IBooksService _booksService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IDomainFactory domainFactory, IBooksService booksService, ILogger<BooksController> logger)
        {
            _domainFactory = domainFactory;
            _booksService = booksService;
            _logger = logger;
        }


        [HttpGet]
        [Route("GetBookDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookDetailsAsync(string bookId)
        {
            try
            {
                // Create valid book
                var book = _domainFactory.CreateBook(bookId);

                var bookDetailsResult = await _booksService.GetBookDetailsAsync(book);

                switch (bookDetailsResult.Status)
                {
                    case StatusEnum.Ok:
                        return Ok(bookDetailsResult.IndividualBookDetails);
                    case StatusEnum.NotFound:
                        return StatusCode(204, bookDetailsResult.Error.Message);
                    case StatusEnum.InvalidParamater:
                        return BadRequest(bookDetailsResult.Error.Message);
                    default:
                        return StatusCode(500, bookDetailsResult.Error.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksController) }", $"Method={ nameof(GetBookDetailsAsync) }");
                return StatusCode(500, ((InvalidBookException)ex).Message);
            }
        }

        [HttpPost]
        [Route("GetBooksCatalog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBooksCatalogAsync(BooksCatalogSearch booksCatalogSearch)
        {
            try
            {
                // Create valid book catalog
                var checkedBooksCatalogSearch = _domainFactory.CreateBooksCatalog(booksCatalogSearch);

                var booksCatalogResult = await _booksService.GetBooksCatalogAsync(checkedBooksCatalogSearch);

                switch (booksCatalogResult.Status)
                {
                    case StatusEnum.Ok:
                        return Ok(booksCatalogResult.BooksCatalog.BookDetails);
                    case StatusEnum.InvalidParamater:
                        return BadRequest(booksCatalogResult.Error.Message);
                    default:
                        return StatusCode(500, booksCatalogResult.Error.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(BooksController) }", $"Method={ nameof(GetBooksCatalogAsync) }");
                return StatusCode(500, ((InvalidBooksCatalogException)ex).Message);
            }
        }

        [HttpPost]
        public async Task AddFavoriteBook(BookDetailsForCatalog book)
        {

        }
    }
}
