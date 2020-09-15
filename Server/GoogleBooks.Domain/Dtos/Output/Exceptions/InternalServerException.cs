namespace GoogleBooks.Domain.Dtos.Output.Exceptions
{
    public class InternalServerException : ErrorBase
    {
        public InternalServerException(string message) : base(message)
        {
        }
    }
}
