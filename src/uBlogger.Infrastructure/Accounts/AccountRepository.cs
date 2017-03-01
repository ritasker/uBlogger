using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using uBlogger.Domain.Entities;
using uBlogger.Infrastructure.Database;

namespace uBlogger.Infrastructure.Accounts
{
    public class AccountRepository
    {
        private readonly CloudTableClient _cloudTableClient;

        public AccountRepository(DatabaseConfiguration config)
        {
            var storageAccount = CloudStorageAccount.Parse(config.ConnectionString);
            _cloudTableClient = storageAccount.CreateCloudTableClient();
        }

        public async Task Save(Account account)
        {
            var tableReference = _cloudTableClient.GetTableReference("AccountByUsername");
            var operation = TableOperation.Insert(new AccountByUsername(account.UserName, account.Email, account.Hash));
            await tableReference.ExecuteAsync(operation);
        }

        public async Task<Account> FindByUsername(string username)
        {
            var tableReference = _cloudTableClient.GetTableReference("AccountByUsername");
            var retrieveOperation = TableOperation.Retrieve<AccountByUsername>(username.Substring(0,3), username);

            var result = await tableReference.ExecuteAsync(retrieveOperation);

            var accountByUsername = result.Result as AccountByUsername;
            return accountByUsername != null
                ? new Account(accountByUsername.Username, accountByUsername.Email, accountByUsername.Hash)
                : null;
        }
    }
}