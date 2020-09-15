namespace GoogleBooks.Domain.Dtos.Output.Exceptions
{
    public class NotFoundException : ErrorBase
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
