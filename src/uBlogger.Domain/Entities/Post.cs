using System;

namespace uBlogger.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid AccountId { get; set; }
        public string Content { get; set; }

        public Post()
        {
            
        }

        public Post(Guid accountId, string content)
        {
            Guard.NotNullOrEmpty(accountId, nameof(accountId));
            Guard.NotNullOrEmpty(content, nameof(content));

            Id = Guid.NewGuid();
            Date = DateTime.UtcNow;
            AccountId = accountId;
            Content = content;
        }
    }
}