namespace uBlogger.Api.Features.Accounts.SignUp
{
    public class SignUpCommand
    {
        public SignUpCommand() {}
        public SignUpCommand(string userName, string email, string hash)
        {
            UserName = userName;
            Email = email;
            Hash = hash;
        }

        public string UserName { get; set;}
        public string Email { get; set;}
        public string Hash { get; set;}
    }
}