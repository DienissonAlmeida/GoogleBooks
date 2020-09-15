using GoogleBooks.Domain.Dtos;

namespace GoogleBooks.Domain.Domain
{
    public interface IDomainFactory
    {
        Book CreateBook(string bookId);

        BooksCatalog CreateBooksCatalog(BooksCatalogSearch booksCatalogSearch);
    }
}