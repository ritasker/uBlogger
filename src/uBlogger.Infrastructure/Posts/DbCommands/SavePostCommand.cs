using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using uBlogger.Domain.Entities;
using uBlogger.Infrastructure.Database;

namespace uBlogger.Infrastructure.Posts.DbCommands
{
    public class SavePostCommand : SqlCommand
    {
        private readonly Post _post;
        public SavePostCommand(Post post)
        {
            _post = post;
            sql = "INSERT INTO public.\"Posts\"(\"Id\", \"AccountId\", \"Date\", \"Content\") VALUES (@Id, @AccountId, @Date, @Content);";
        }
        public override async Task ExecuteAsync(IDbConnection connection)
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    await connection.ExecuteAsync(sql, _post, transaction);
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