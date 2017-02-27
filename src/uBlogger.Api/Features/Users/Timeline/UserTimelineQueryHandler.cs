using System.Collections.Generic;
using MediatR;
using uBlogger.Domain.Entities;
using uBlogger.Infrastructure.Posts;

namespace uBlogger.Api.Features.Posts.Timeline
{
    public class UserTimelineQueryHandler : IRequestHandler<UserTimelineQuery, IEnumerable<Post>>
    {
        private readonly PostRepository _postRepository;

        public UserTimelineQueryHandler(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public IEnumerable<Post> Handle(UserTimelineQuery message)
        {
            return _postRepository.UserTimeline(message.AccountId).GetAwaiter().GetResult();
        }
    }
}