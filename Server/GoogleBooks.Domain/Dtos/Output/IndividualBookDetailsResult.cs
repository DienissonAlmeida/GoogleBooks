using GoogleBooks.Domain.Dtos.Output.Exceptions;

namespace GoogleBooks.Domain.Dtos.Output
{
    public class IndividualBookDetailsResult : ResultBase
    {
        public IndividualBookDetails IndividualBookDetails { get; private set; }

        public IndividualBookDetailsResult(IndividualBookDetails individualBookDetails, StatusEnum status) : base(status)
        {
            IndividualBookDetails = individualBookDetails;
        }

        public IndividualBookDetailsResult(ErrorBase error, StatusEnum status) : base(error, status)
        {
        }
    }
}
