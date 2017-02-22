using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using uBlogger.Domain.Entities;
using uBlogger.Infrastructure.Database;

namespace uBlogger.Infrastructure.Posts
{
    public class PostsByUsernameQuery : SqlQuery<IEnumerable<Post>>
    {
        private readonly string username;

        public PostsByUsernameQuery(string username)
        {
            this.username = username;
            sql = "SELECT \"Posts\".\"Id\", \"AccountId\", \"Date\", \"Content\" FROM \"Posts\" INNER JOIN \"Accounts\" ON \"Accounts\".\"Id\" = \"Posts\".\"AccountId\" WHERE \"Accounts\".\"UserName\" = @Username";
        }

        public override async Task<IEnumerable<Post>> QueryAsync(IDbConnection connection)
        {
            return await connection.QueryAsync<Post>(sql, new { Username = username });
        }
    }
}