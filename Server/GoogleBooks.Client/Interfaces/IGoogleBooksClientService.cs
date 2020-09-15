using GoogleBooks.Domain.Domain;
using GoogleBooks.Client.Dtos.Output;
using System.Threading.Tasks;

namespace GoogleBooks.Client.Interfaces
{
    public interface IGoogleBooksClientService
    {
        Task<GoogleBookDetailsFull> GetBookDetailsAsync(string bookId);

        Task<GoogleBooksCatalog> GetBooksCatalogAsync(BooksCatalog domainBooksCatalog);
    }
}
