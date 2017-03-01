using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace uBlogger.Infrastructure.Posts.TableEntities
{
    public class UserPost : TableEntity
    {
        public UserPost() { }
        public UserPost(string username, Guid id, string content)
        {
            Content = content;
            PartitionKey = username;
            RowKey = id.ToString();
        }

        public string Content { get; }
    }
}