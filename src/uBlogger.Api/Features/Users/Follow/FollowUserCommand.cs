using System;
using MediatR;

namespace uBlogger.Api.Features.Users.Follow
{
    public class FollowUserCommand : IRequest
    {
        public FollowUserCommand(Guid accountId, string username)
        {
            AccountId = accountId;
            Username = username;
        }
        
        public Guid AccountId { get; set; }
        public string Username { get; set; }
    }
}