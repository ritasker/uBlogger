using System;

namespace uBlogger.Infrastructure.Database
{
    public class TransactionFailedException : Exception
    {
        public TransactionFailedException(Type sqlCommand, Exception exception) : base($"Transaction Failed For {sqlCommand.Name}. Parameters: {exception.Data}", exception)
        {
        }
    }
}