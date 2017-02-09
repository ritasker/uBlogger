using uBlogger.Domain.Accounts;
using uBlogger.Infrastructure.Database;

namespace uBlogger.Infrastructure.Accounts
{
    public class AccountRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public AccountRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }
        public void Save(Account account)
        {
            using (var connection = _dbConnectionProvider.GetConnection())
            {
                var command = new SaveAccountCommand(account.Id, account.Name, account.Email);
                command.Execute(connection);
            }
        }
    }
}