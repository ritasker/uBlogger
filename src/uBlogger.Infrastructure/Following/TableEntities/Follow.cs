using Microsoft.WindowsAzure.Storage.Table;

namespace uBlogger.Infrastructure.Following.TableEntities
{
    public class Follow : TableEntity
    {
        public Follow(string followee, string follower)
        {
            PartitionKey = followee;
            RowKey = follower;
        }

        public string FolloweeUsername => PartitionKey;
        public string FollowerUsername => RowKey;
    }
}