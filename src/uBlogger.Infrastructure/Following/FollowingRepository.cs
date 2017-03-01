using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using uBlogger.Infrastructure.Database;
using uBlogger.Infrastructure.Following.TableEntities;

namespace uBlogger.Infrastructure.Following
{
    public class FollowingRepository
    {
        private readonly CloudTableClient _cloudTableClient;

        public FollowingRepository(DatabaseConfiguration config)
        {
            var storageAccount = CloudStorageAccount.Parse(config.ConnectionString);
            _cloudTableClient = storageAccount.CreateCloudTableClient();
        }

        public async Task Save(string follower, string followee)
        {
            var followingTable = _cloudTableClient.GetTableReference("Following");
            var followingOp = TableOperation.Insert(new Follow(follower, followee));
            await followingTable.ExecuteAsync(followingOp);

            var followersTable = _cloudTableClient.GetTableReference("Followers");
            var followersOp = TableOperation.Insert(new Follow(followee, follower));
            await followersTable.ExecuteAsync(followersOp);
        }
    }
}