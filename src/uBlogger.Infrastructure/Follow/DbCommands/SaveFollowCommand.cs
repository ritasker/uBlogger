using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using uBlogger.Infrastructure.Database;

namespace uBlogger.Infrastructure.Follow.DbCommands
{
    public class SaveFollowCommand : SqlCommand
    {
        private readonly Domain.Entities.Follow follow;

        public SaveFollowCommand(Domain.Entities.Follow follow)
        {
            this.follow = follow;
            sql = "INSERT INTO public.\"Follows\"(\"FollowerId\", \"AccountId\") VALUES (@FollowerId, @AccountId);";
        }

        public override async Task ExecuteAsync(IDbConnection connection)
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    await connection.ExecuteAsync(sql, follow, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new TransactionFailedException(GetType(), ex);
                }
            }
        }
    }
}