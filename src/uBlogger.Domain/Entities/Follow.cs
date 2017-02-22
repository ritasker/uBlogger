using System;

namespace uBlogger.Domain.Entities
{
    public class Follow
    {
        public Guid FollowerId { get; set; }
        public Guid AccountId { get; set; }

        public Follow(Guid followerId, Guid accountId)
        {
            FollowerId = followerId;
            AccountId = accountId;
        }
    }
}