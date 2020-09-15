using GoogleBooks.Domain.Dtos.Output.Exceptions;
using GoogleBooks.Domain.Helpers;

namespace GoogleBooks.Domain.Domain
{
    public class Book
    {
        private const int bookIdLength = 12;

        public string Id { get; private set; }

        public Book(string bookId)
        {
            if (string.IsNullOrWhiteSpace(bookId))
            {
                throw new InvalidBookException(ExceptionMessages.EmptyId);
            }

            if (bookId.Length != bookIdLength)
            {
                throw new InvalidBookException(ExceptionMessages.InvalidIdLength);
            }

            Id = bookId;
        }
    }
}
