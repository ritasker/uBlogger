using System;
using MediatR;

namespace uBlogger.Api.Features.Users.Post
{
    public class AddPostCommand : IRequest
    {
        public string Username { get; }
        public string Content { get; }
        public Guid Id { get; }

        public AddPostCommand(Guid id, string username, string content)
        {
            Id = id;
            Username = username;
            Content = content;
        }
    }
}