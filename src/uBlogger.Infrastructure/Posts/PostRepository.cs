using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using uBlogger.Infrastructure.Database;
using uBlogger.Infrastructure.Following.TableEntities;
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

        public async Task Save(Guid id, string username, string content)
        {
            // Insert into my timeline
            var userPostsTable = _cloudTableClient.GetTableReference("UserPosts");
            var userPostOp = TableOperation.Insert(new UserPost(username, id, content));
            await userPostsTable.ExecuteAsync(userPostOp);

            // Insert into my followers timelines
            var followersTable = _cloudTableClient.GetTableReference("Followers");
            var query = new TableQuery<Follow>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, username)
            );

            TableContinuationToken token = null;
            var batchOperation = new TableBatchOperation();
            var table = _cloudTableClient.GetTableReference("UserTimeline");

            do
            {
                var result = await followersTable.ExecuteQuerySegmentedAsync(query, token);

                batchOperation.Clear();
                result.Results.ForEach(x => batchOperation.Insert(new UserTimeline(x.Username2, id, username, content)));
                table.ExecuteBatchAsync(batchOperation);

                token = result.ContinuationToken;
            } while (token != null);
        }

        public async Task<IEnumerable<UserPost>> PostsByUser(string username)
        {
            var table = _cloudTableClient.GetTableReference("UserPosts");
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