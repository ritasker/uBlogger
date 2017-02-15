using System.Threading.Tasks;
using uBlogger.Domain.Accounts;
using uBlogger.Infrastructure.Accounts.DbCommands;
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
        public async Task Save(Account account)
        {
            using (var connection = _dbConnectionProvider.GetConnection())
            {
                var command = new SaveAccountCommand(account.Id, account.Name, account.Email);
                await command.ExecuteAsync(connection);
            }
        }
    }
}