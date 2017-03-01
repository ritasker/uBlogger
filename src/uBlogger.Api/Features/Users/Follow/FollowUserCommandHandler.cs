using MediatR;
using uBlogger.Infrastructure.Following;

namespace uBlogger.Api.Features.Users.Follow
{
    public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand>
    {
        private readonly FollowingRepository followingRepository;

        public FollowUserCommandHandler(FollowingRepository followingRepository)
        {
            this.followingRepository = followingRepository;
        }

        public void Handle(FollowUserCommand message)
        {
            followingRepository.Save(message.Follower, message.Followee).GetAwaiter();
        }
    }
}