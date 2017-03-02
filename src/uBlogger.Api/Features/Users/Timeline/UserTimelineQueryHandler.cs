using System.Collections.Generic;
using MediatR;
using uBlogger.Infrastructure.Posts;
using uBlogger.Infrastructure.Posts.TableEntities;

namespace uBlogger.Api.Features.Users.Timeline
{
    public class UserTimelineQueryHandler : IRequestHandler<UserTimelineQuery, IEnumerable<UserTimeline>>
    {
        private readonly PostRepository _postRepository;

        public UserTimelineQueryHandler(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public IEnumerable<UserTimeline> Handle(UserTimelineQuery message)
        {
            return _postRepository.UserTimeline(message.Username).GetAwaiter().GetResult();
        }
    }
}