using MediatR;

namespace uBlogger.Api.Features.Accounts.SignUp
{
    public class SignUpCommand : IRequest
    {
        public SignUpCommand(string userName, string email, string password)
        {
            UserName = userName;
            Email = email;
            Password = password;
        }

        public string UserName { get; }
        public string Email { get; }
        public string Password { get; }
    }
}