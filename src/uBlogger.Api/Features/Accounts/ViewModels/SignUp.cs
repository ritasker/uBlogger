using FluentValidation;

namespace uBlogger.Api.Features.Accounts.ViewModels
{
    public class SignUp
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class SignUpValidator : AbstractValidator<SignUp>
    {
        public SignUpValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Please include your name.");
            RuleFor(m => m.Email).NotEmpty().EmailAddress().WithMessage("An valid email address is needed.");
        }
    }
}