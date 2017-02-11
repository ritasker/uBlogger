using MediatR;
using Nancy;
using Nancy.Configuration;
using Nancy.Conventions;
using Nancy.TinyIoc;
using uBlogger.Api.Features.Accounts.SignUp;
using uBlogger.Infrastructure.Accounts;
using uBlogger.Infrastructure.Database;

namespace uBlogger.Api
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private readonly ApiConfiguration apiConfiguration;

        public Bootstrapper(ApiConfiguration apiConfiguration)
        {
            this.apiConfiguration = apiConfiguration;
        }

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

            container.Register(apiConfiguration.Database);

            container.Register<IDbConnectionProvider, PostgresConnectionProvider>();

            container.Register<AccountRepository>();

            container.Register<IRequestHandler<SignUpCommand, Unit>, SignUpCommandHandler>();

            RegisterMediatR(container);


        }

        private static void RegisterMediatR(TinyIoCContainer container)
        {
            container.Register<SingleInstanceFactory>((c, p) => c.Resolve);
            container.Register<MultiInstanceFactory>((c, p) => c.ResolveAll);
            container.Register<IMediator, Mediator>();
        }

        public override void Configure(INancyEnvironment environment)
        {
            base.Configure(environment);

            environment.Tracing(false, true);
        }
    }
}