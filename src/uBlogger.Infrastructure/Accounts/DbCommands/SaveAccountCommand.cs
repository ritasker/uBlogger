using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using uBlogger.Infrastructure.Database;

namespace uBlogger.Infrastructure.Accounts.DbCommands
{
    public class SaveAccountCommand : SqlCommand
    {
        private readonly Guid _id;
        private readonly string _name;
        private readonly string _email;

        public SaveAccountCommand(Guid id, string name, string email)
        {
            _id = id;
            _name = name;
            _email = email;

            sql = "INSERT INTO public.\"Accounts\"(\"Id\", \"Name\", \"Email\") VALUES (@Id, @Name, @Email);";
        }


        public override async Task ExecuteAsync(IDbConnection connection)
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    await connection.ExecuteAsync(sql, new { Id = _id, Name = _name, Email = _email }, transaction);
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