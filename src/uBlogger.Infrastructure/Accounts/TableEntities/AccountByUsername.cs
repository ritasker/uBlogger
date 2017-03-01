using Microsoft.WindowsAzure.Storage.Table;

namespace uBlogger.Domain.Entities
{
    public class AccountByUsername : TableEntity
    {
        public string Email { get; }
        public string Hash { get; }
        public string Username => RowKey;

        public AccountByUsername(string userName, string email, string hash)
        {
            PartitionKey = userName.Substring(0, 3);
            RowKey = userName;
            Email = email;
            Hash = hash;
        }
    }
}