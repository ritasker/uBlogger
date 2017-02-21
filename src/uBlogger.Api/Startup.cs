using Microsoft.Extensions.DependencyInjection;
using uBlogger.Infrastructure.Security;

namespace uBlogger.Api
{
    using Microsoft.AspNetCore.Builder;
    using Nancy.Owin;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<HashingService>();
        }
        public void Configure(IApplicationBuilder app, Bootstrapper bootstrapper)
        {
            app.UseOwin(x => x.UseNancy(options => { options.Bootstrapper = bootstrapper; }));
        }
    }
}
