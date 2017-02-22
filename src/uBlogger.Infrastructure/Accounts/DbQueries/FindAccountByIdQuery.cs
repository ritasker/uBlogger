using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using uBlogger.Domain.Entities;
using uBlogger.Infrastructure.Database;

namespace uBlogger.Infrastructure.Accounts.DbQueries
{
    public class FindAccountByIdQuery : SqlQuery<Account>
    {
        private readonly Guid _id;

        public FindAccountByIdQuery(Guid id)
        {
            _id = id;
            sql = sql = "SELECT \"Id\", \"UserName\", \"Email\", \"Hash\" FROM public.\"Accounts\" WHERE \"Id\" = @Id;";
        }

        public override async Task<Account> QueryAsync(IDbConnection connection)
        {
            var results = await connection.QueryAsync<Account>(sql, new {Id = _id});
            return results.Single();
        }
    }
}