using GoogleBooks.Domain.Dtos.Output;

namespace GoogleBooks.Domain.Dtos.Output
{
    public class BooksCatalogSearchResult
    {
        public PagingCatalogResult PagingInfoResult { get; private set; }

        public BooksCatalog BooksCatalog { get; private set; }

        public BooksCatalogSearchResult(PagingCatalogResult pagingInfoResult, BooksCatalog booksCatalog)
        {
            PagingInfoResult = pagingInfoResult;
            BooksCatalog = booksCatalog;
        }
    }
}
