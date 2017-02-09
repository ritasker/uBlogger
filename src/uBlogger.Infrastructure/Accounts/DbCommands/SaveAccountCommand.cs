using System;
using System.Data;
using Dapper;
using uBlogger.Infrastructure.Database;

namespace uBlogger.Infrastructure.Accounts
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
            sql = "INSERT INTO Accounts VALUES (@Id, @Name, @Email);";
        }


        public override void Execute(IDbConnection connection)
        {
            using (var transaction = connection.BeginTransaction())
            {
                connection.ExecuteAsync(sql, new { Id = _id, Name = _name, Email = _email }, transaction);
                transaction.Commit();
            }
        }
    }
}