using System;
using System.Threading.Tasks;
using uBlogger.Domain.Entities;
using uBlogger.Infrastructure.Accounts.DbCommands;
using uBlogger.Infrastructure.Accounts.DbQueries;
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
                var command = new SaveAccountCommand(account);
                await command.ExecuteAsync(connection);
            }
        }

        public async Task<Account> FindByUsername(string username)
        {
            using (var connection = _dbConnectionProvider.GetConnection())
            {
                var query = new FindAccountByUsernameQuery(username);
                return await query.QueryAsync(connection);
            }
        }

        public async Task<Account> FindById(Guid accountId)
        {
            using (var connection = _dbConnectionProvider.GetConnection())
            {
                var query = new FindAccountByIdQuery(accountId);
                return await query.QueryAsync(connection);
            }
        }
    }
}