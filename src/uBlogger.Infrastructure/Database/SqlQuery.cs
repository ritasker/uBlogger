using System.Data;
using System.Threading.Tasks;

namespace uBlogger.Infrastructure.Database
{
    public abstract class SqlQuery<T>
    {
        protected string sql;
        public abstract Task<T> QueryAsync(IDbConnection connection);
    }
}