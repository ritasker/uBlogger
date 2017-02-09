using MediatR;
using Nancy;
using Nancy.Configuration;
using Nancy.Conventions;
using Nancy.TinyIoc;
using uBlogger.Api.Features.Accounts.SignUp;

namespace uBlogger.Api
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.ViewLocationConventions.Clear();

            nancyConventions.ViewLocationConventions.Add(
                (viewName, model, viewLocationContext) =>
                    "features/" + viewName);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<IRequestHandler<SignUpCommand, Unit>>();

            container.Register(new SingleInstanceFactory(container.Resolve));
            container.Register(new MultiInstanceFactory(container.ResolveAll));
        }

        public override void Configure(INancyEnvironment environment)
        {
            base.Configure(environment);

            environment.Tracing(enabled: false, displayErrorTraces: true);
        }
    }
}