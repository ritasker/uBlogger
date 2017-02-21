using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using uBlogger.Domain.Entities;
using uBlogger.Infrastructure.Database;

namespace uBlogger.Infrastructure.Accounts.DbQueries
{
    public class FindAccountByUsernameQuery : SqlQuery<Account>
    {
        private readonly string _username;

        public FindAccountByUsernameQuery(string username)
        {
            _username = username;
            sql = "SELECT \"Id\", \"UserName\", \"Email\", \"Hash\" FROM public.\"Accounts\" WHERE \"UserName\" = @Username;";
        }
        public override async Task<Account> QueryAsync(IDbConnection connection)
        {
            return connection.QueryAsync<Account>(sql, new {UserName = _username}).GetAwaiter().GetResult().Single();
        }
    }
}