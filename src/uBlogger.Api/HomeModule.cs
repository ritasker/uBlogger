namespace uBlogger.Api
{
    using Nancy;

    public class HomeModule : NancyModule
    {
        public HomeModule() : base("/api")
        {
            Get("/", _ => "Hello World");
        }
    }
}
