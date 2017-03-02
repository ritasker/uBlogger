using System;

namespace uBlogger.Api.Features.Users
{
    public class PostViewModel
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
    }
}