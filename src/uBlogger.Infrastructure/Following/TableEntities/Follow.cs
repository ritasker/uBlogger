using Microsoft.WindowsAzure.Storage.Table;

namespace uBlogger.Infrastructure.Following.TableEntities
{
    public class Follow : TableEntity
    {
        public Follow(){ }
        public Follow(string username1, string username2)
        {
            PartitionKey = username1;
            RowKey = username2;
        }

        public string Username1 => PartitionKey;
        public string Username2 => RowKey;
    }
}