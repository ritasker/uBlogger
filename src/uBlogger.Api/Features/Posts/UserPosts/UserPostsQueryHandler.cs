using System.Collections.Generic;
using MediatR;
using uBlogger.Infrastructure.Posts;
using uBlogger.Infrastructure.Posts.TableEntities;

namespace uBlogger.Api.Features.Posts.UserPosts
{
    public class UserPostsQueryHandler : IRequestHandler<UserPostsQuery, IEnumerable<UserPost>>
    {
        private readonly PostRepository postRepository;

        public UserPostsQueryHandler(PostRepository postRepository)
        {
            this.postRepository = postRepository;
        }
        public IEnumerable<UserPost> Handle(UserPostsQuery message)
        {
            return postRepository.PostsByUser(message.Username).GetAwaiter().GetResult();
        }
    }
}