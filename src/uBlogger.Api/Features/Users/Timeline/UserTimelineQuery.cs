using System.Collections.Generic;
using MediatR;
using uBlogger.Infrastructure.Posts.TableEntities;

namespace uBlogger.Api.Features.Users.Timeline
{
    public class UserTimelineQuery : IRequest<IEnumerable<UserTimeline>>
    {
        public UserTimelineQuery(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
    }
}