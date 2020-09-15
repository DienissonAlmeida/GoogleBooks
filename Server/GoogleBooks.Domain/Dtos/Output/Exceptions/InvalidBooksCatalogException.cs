namespace GoogleBooks.Domain.Dtos.Output.Exceptions
{
    public class InvalidBooksCatalogException : ErrorBase
    {
        public InvalidBooksCatalogException(string message) : base(message)
        {
        }
    }
}
