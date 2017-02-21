using System;
using System.Text;

namespace uBlogger.Api.Authorization
{
    public class Credentials
    {
        public Credentials(string authToken)
        {
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
            var separator = credentials.IndexOf(':');

            Username = credentials.Substring(0, separator);
            Password = credentials.Substring(separator + 1);
        }

        public string Username { get; }
        public string Password { get; }
    }
}