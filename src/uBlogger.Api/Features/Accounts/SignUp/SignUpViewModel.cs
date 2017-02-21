using FluentValidation;

namespace uBlogger.Api.Features.Accounts.SignUp
{
    public class SignUpViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignUpValidator : AbstractValidator<SignUpViewModel>
    {
        public SignUpValidator()
        {
            RuleFor(m => m.Username).NotEmpty().WithMessage("Please select a username.");
            RuleFor(m => m.Email).NotEmpty().EmailAddress().WithMessage("An valid email address is needed.");
            RuleFor(m => m.Password).NotEmpty().WithMessage("Please include a password.");
        }
    }
}