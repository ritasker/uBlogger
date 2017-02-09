using System.Data;

namespace uBlogger.Infrastructure.Database
{
    public abstract class SqlCommand
    {
        protected string sql;

        public abstract void Execute(IDbConnection connection);
    }
}