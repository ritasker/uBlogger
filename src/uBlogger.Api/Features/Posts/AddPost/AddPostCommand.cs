using System;
using MediatR;

namespace uBlogger.Api.Features.Posts
{
    public class AddPostCommand : IRequest<Guid>
    {
        public Guid AccountId { get; }
        public string Content { get; }

        public AddPostCommand(Guid accountId, string content)
        {
            AccountId = accountId;
            Content = content;
        }
    }
}