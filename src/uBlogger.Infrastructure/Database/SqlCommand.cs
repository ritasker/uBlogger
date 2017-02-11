using System.Data;
using System.Threading.Tasks;

namespace uBlogger.Infrastructure.Database
{
    public abstract class SqlCommand
    {
        protected string sql;

        public abstract Task ExecuteAsync(IDbConnection connection);
    }
}