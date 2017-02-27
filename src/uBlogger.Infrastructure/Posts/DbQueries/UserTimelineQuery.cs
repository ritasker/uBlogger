using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using uBlogger.Domain.Entities;
using uBlogger.Infrastructure.Database;

namespace uBlogger.Infrastructure.Posts
{
    public class UserTimelineQuery : SqlQuery<IEnumerable<Post>>
    {
        private readonly Guid accountId;

        public UserTimelineQuery(Guid accountId)
        {
            this.accountId = accountId;
            sql = "SELECT \"Id\", \"Posts\".\"AccountId\", \"Date\", \"Content\" FROM public.\"Posts\" " +
                  "INNER JOIN \"Follows\" ON \"Follows\".\"AccountId\" = \"Posts\".\"AccountId\" " +
                  "WHERE \"Follows\".\"FollowerId\" = @Id;";
        }

        public override async Task<IEnumerable<Post>> QueryAsync(IDbConnection connection)
        {
            return await connection.QueryAsync<Post>(sql, new { Id = accountId });
        }
    }
}