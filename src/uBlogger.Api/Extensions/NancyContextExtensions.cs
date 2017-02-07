using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Authentication;
using Nancy;

namespace uBlogger.Api.Extensions
{
    public static class NancyContextExtensions
    {
        public static AuthenticationManager GetAuthenticationManager(this NancyContext context)
        {
            object requestEnvironment;
            context.Items.TryGetValue(Nancy.Owin.NancyMiddleware.RequestEnvironmentKey, out requestEnvironment);
            var environment = requestEnvironment as IDictionary<string, object>;

            try
            {
                var httpContext = (Microsoft.AspNetCore.Http.HttpContext)environment["Microsoft.AspNetCore.Http.HttpContext"];
                return httpContext.Authentication;
            }
            catch (KeyNotFoundException)
            {
                try
                {
                    var defaultcontext = (Microsoft.AspNetCore.Http.DefaultHttpContext)environment["Microsoft.AspNetCore.Http.DefaultHttpContext"];
                    return defaultcontext.Authentication;
                }
                catch (KeyNotFoundException)
                {
                    return null;
                }
            }
        }
    }
}