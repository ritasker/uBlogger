using FluentValidation;

namespace uBlogger.Api.Features.Users.Follow
{
    public class FollowUserViewModel
    {
        public string Username { get; set; }
    }

    public class FollowUserViewModelValidator : AbstractValidator<FollowUserViewModel>
    {
        public FollowUserViewModelValidator()
        {
            RuleFor(m => m.Username).NotEmpty();
        }
    }
}