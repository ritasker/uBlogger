using Microsoft.WindowsAzure.Storage.Table;

namespace uBlogger.Infrastructure.Accounts.TableEntities
{
    public class AccountByUsername : TableEntity
    {
        public AccountByUsername() { }
        public AccountByUsername(string userName, string email, string hash)
        {
            PartitionKey = userName.Substring(0, 3);
            RowKey = userName;
            Email = email;
            Hash = hash;
        }

        public string Email { get; set; }
        public string Hash { get; set;  }
        public string Username => RowKey;
    }
}