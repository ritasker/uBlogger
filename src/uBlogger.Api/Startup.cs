using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace uBlogger.Api
{
    using Microsoft.AspNetCore.Builder;
    using Nancy.Owin;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication();
            //services.AddDataProtection();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                LoginPath = new PathString("/Account/Login")
            });

            app.UseOwin(x => x.UseNancy());
        }
    }
}
