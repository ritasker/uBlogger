namespace uBlogger.Domain.Entities
{
    public class Account
    {
        public Account() {}
        public Account(string userName, string email, string hash)
        {
            Guard.NotNullOrEmpty(userName, nameof(userName));
            Guard.NotNullOrEmpty(email, nameof(email));
            Guard.NotNullOrEmpty(hash, nameof(hash));

            UserName = userName;
            Email = email;
            Hash = hash;
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
    }
}