using System.Collections.Generic;
using MediatR;
using uBlogger.Domain.Entities;

namespace uBlogger.Api.Features.Posts.UserPosts
{
    public class UserPostsQuery : IRequest<IEnumerable<Post>>
    {
        public UserPostsQuery(string username)
        {
            Username = username;
        }

        public string Username { get; }
    }
}