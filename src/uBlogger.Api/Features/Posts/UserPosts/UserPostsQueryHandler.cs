using System.Collections.Generic;
using MediatR;
using uBlogger.Domain.Entities;
using uBlogger.Infrastructure.Posts;

namespace uBlogger.Api.Features.Posts.UserPosts
{
    public class UserPostsQueryHandler : IRequestHandler<UserPostsQuery, IEnumerable<Post>>
    {
        private readonly PostRepository postRepository;

        public UserPostsQueryHandler(PostRepository postRepository)
        {
            this.postRepository = postRepository;
        }
        public IEnumerable<Post> Handle(UserPostsQuery message)
        {
            return postRepository.FindByUsername(message.Username).GetAwaiter().GetResult();
        }
    }
}