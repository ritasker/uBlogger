using System;

namespace uBlogger.Api.Features.Users.Post
{
    public class PostContentCommand
    {
        public PostContentCommand() { }
        public PostContentCommand(Guid id, string author, string content)
        {
            Id = id;
            Author = author;
            Content = content;
        }

        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }

    }
}