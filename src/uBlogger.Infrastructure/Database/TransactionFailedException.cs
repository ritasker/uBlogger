using System;

namespace uBlogger.Infrastructure.Accounts.DbCommands
{
    public class TransactionFailedException : Exception
    {
        public TransactionFailedException(Type sqlCommand, Exception exception) : base($"Transaction Failed For {sqlCommand.Name}. Parameters: {exception.Data}", exception)
        {
        }
    }
}