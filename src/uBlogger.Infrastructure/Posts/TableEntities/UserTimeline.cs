using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace uBlogger.Infrastructure.Posts.TableEntities
{
    public class UserTimeline : TableEntity
    {
        public UserTimeline() { }
        public UserTimeline(string username, Guid id, string author, string content)
        {
            Author = author;
            Content = content;
            PartitionKey = username;
            RowKey = id.ToString();
        }

        public string Author { get; set; }
        public string Content { get; set; }
    }
}