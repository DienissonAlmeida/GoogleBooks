using System;

namespace GoogleBooks.Domain.Dtos.Output.Exceptions
{
    public abstract class ErrorBase : Exception
    {
        public string Message { get; private set; }

        protected ErrorBase(string message)
        {
            Message = message;
        }
    }
}
