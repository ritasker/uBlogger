using MediatR;
using uBlogger.Infrastructure.Accounts;
using uBlogger.Infrastructure.Follow;

namespace uBlogger.Api.Features.Users.Follow
{
    public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand>
    {
        private readonly AccountRepository accountRepository;
        private readonly FollowRepository followRepository;

        public FollowUserCommandHandler(AccountRepository accountRepository, FollowRepository followRepository)
        {
            this.accountRepository = accountRepository;
            this.followRepository = followRepository;
        }
        public void Handle(FollowUserCommand message)
        {
            var account = accountRepository.FindByUsername(message.Username).GetAwaiter().GetResult();
            var follow = new Domain.Entities.Follow(message.Username, account.UserName);
            followRepository.Save(follow).GetAwaiter();
        }
    }
}