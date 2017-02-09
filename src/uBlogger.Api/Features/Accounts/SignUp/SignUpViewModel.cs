using FluentValidation;

namespace uBlogger.Api.Features.Accounts.SignUp
{
    public class SignUpViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class SignUpValidator : AbstractValidator<SignUpViewModel>
    {
        public SignUpValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Please include your name.");
            RuleFor(m => m.Email).NotEmpty().EmailAddress().WithMessage("An valid email address is needed.");
        }
    }
}