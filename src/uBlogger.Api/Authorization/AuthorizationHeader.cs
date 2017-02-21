using Microsoft.Extensions.Primitives;

namespace uBlogger.Api.Authorization
{
    public class AuthorizationHeader
    {
        public AuthorizationHeader(StringValues header)
        {
            var segments = header[0].Split(' ');
            Scheme = segments[0];
            Parameter = segments[1];
        }

        public string Scheme { get; }
        public string Parameter { get; }
    }
}