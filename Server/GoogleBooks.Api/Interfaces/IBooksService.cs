using GoogleBooks.Domain.Domain;
using GoogleBooks.Domain.Dtos.Output;
using System.Threading.Tasks;
using BooksCatalog = GoogleBooks.Domain.Domain.BooksCatalog;

namespace GoogleBooks.Api.Interfaces
{
    public interface IBooksService
    {
        Task<IndividualBookDetailsResult> GetBookDetailsAsync(Book book);

        Task<BooksCatalogResult> GetBooksCatalogAsync(BooksCatalog catalogBooksSearch);
    }
}