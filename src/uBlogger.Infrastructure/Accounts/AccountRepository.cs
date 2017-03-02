using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using uBlogger.Infrastructure.Accounts.TableEntities;
using uBlogger.Infrastructure.DataAccess;

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

        public async Task Save(string userName, string email, string hash)
        {
            var tableReference = _cloudTableClient.GetTableReference("AccountByUsername");
            var operation = TableOperation.Insert(new AccountByUsername(userName, email, hash));
            await tableReference.ExecuteAsync(operation);
        }

        public async Task<AccountByUsername> FindByUsername(string username)
        {
            var tableReference = _cloudTableClient.GetTableReference("AccountByUsername");
            var retrieveOperation = TableOperation.Retrieve<AccountByUsername>(username.Substring(0,3), username);

            var result = await tableReference.ExecuteAsync(retrieveOperation);

            return result.Result as AccountByUsername;
        }
    }
}