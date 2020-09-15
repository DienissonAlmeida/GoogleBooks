using GoogleBooks.Domain.Dtos.Output.Exceptions;

namespace GoogleBooks.Domain.Dtos.Output
{
    public class BooksCatalogResult : ResultBase
    {
        public PagingCatalogResult PagingInfo { get; private set; }

        public BooksCatalog BooksCatalog { get; private set; }

        public BooksCatalogResult(BooksCatalogSearchResult booksCatalogSearchResult, StatusEnum status) : base(status)
        {
            BooksCatalog = booksCatalogSearchResult.BooksCatalog;
            PagingInfo = booksCatalogSearchResult.PagingInfoResult;
        }

        public BooksCatalogResult(ErrorBase error, StatusEnum status) : base(error, status)
        {
        }
    }
}
