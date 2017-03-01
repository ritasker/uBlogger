using System.Collections.Generic;
using MediatR;
using uBlogger.Infrastructure.Posts.TableEntities;

namespace uBlogger.Api.Features.Posts.UserPosts
{
    public class UserPostsQuery : IRequest<IEnumerable<UserPost>>
    {
        public UserPostsQuery(string username)
        {
            Username = username;
        }

        public string Username { get; }
    }
}