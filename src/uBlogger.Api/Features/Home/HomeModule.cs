using Nancy;

namespace uBlogger.Api.Features.Home
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Post("SignUp", args => 201);
        }
    }
}
