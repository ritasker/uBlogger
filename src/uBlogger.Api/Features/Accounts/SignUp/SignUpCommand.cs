using MediatR;

namespace uBlogger.Api.Features.Accounts.SignUp
{
    public class SignUpCommand : IRequest<Unit>
    {
        public SignUpCommand(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
    }
}