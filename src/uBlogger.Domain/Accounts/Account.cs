using System;

namespace uBlogger.Domain.Accounts
{
    public class Account
    {
        public Guid Id { get;  }
        public string Name { get; }
        public string Email { get; }

        public Account(string name, string email)
        {
            Guard.NotNullOrEmpty(name, nameof(name));
            Guard.NotNullOrEmpty(email, nameof(email));

            Id = Guid.NewGuid();
            Name = name;
            Email = email;
        }
    }
}