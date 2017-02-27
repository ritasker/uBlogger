using System;
using System.Collections.Generic;
using MediatR;
using uBlogger.Domain.Entities;

namespace uBlogger.Api.Features.Posts.Timeline
{
    public class UserTimelineQuery : IRequest<IEnumerable<Post>>
    {
        public UserTimelineQuery(Guid accountId)
        {
            AccountId = accountId;
        }

        public Guid AccountId { get; set; }
    }
}