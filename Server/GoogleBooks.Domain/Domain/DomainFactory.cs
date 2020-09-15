using GoogleBooks.Domain.Dtos;

namespace GoogleBooks.Domain.Domain
{
    public class DomainFactory : IDomainFactory
    {
        public Book CreateBook(string bookId)
        {
            return new Book(bookId);
        }

        public BooksCatalog CreateBooksCatalog(BooksCatalogSearch booksCatalogSearch)
            => new BooksCatalog(booksCatalogSearch.Keywords, booksCatalogSearch.PageNumber, booksCatalogSearch.PageSize);
    }
}
