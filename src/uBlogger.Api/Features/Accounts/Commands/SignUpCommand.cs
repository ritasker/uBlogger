namespace uBlogger.Api.Features.Accounts.Commands
{
    public class SignUpCommand
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