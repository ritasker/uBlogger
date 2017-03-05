using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using uBlogger.Infrastructure.DataAccess;
using uBlogger.Infrastructure.Posts.TableEntities;

namespace uBlogger.Infrastructure.Posts
{
    public class PostRepository
    {
        private readonly CloudTableClient _cloudTableClient;

        public PostRepository(DatabaseConfiguration config)
        {
            var storageAccount = CloudStorageAccount.Parse(config.ConnectionString);
            _cloudTableClient = storageAccount.CreateCloudTableClient();
        }

        public async Task<IEnumerable<UserPost>> PostsByUser(string username)
        {
            var table = _cloudTableClient.GetTableReference("PostsByUser");
            var query = new TableQuery<UserPost>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, username)
            ).Take(50);

            var result = await table.ExecuteQuerySegmentedAsync(query, null);

            return result.Results;
        }

        public async Task<IEnumerable<UserTimeline>> UserTimeline(string username)
        {
            var table = _cloudTableClient.GetTableReference("UserTimeline");
            var query = new TableQuery<UserTimeline>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, username)
            ).Take(50);

            var result = await table.ExecuteQuerySegmentedAsync(query, null);

            return result.Results;
        }
    }
}