using System;
using System.Collections.Generic;
using System.Security.Claims;
using MediatR;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.Conventions;
using Nancy.TinyIoc;
using uBlogger.Api.Authorization;
using uBlogger.Api.Features.Accounts.SignUp;
using uBlogger.Api.Features.Posts;
using uBlogger.Infrastructure;
using uBlogger.Infrastructure.Accounts;
using uBlogger.Infrastructure.Database;
using uBlogger.Infrastructure.Email;
using uBlogger.Infrastructure.Security;

namespace uBlogger.Api
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private readonly ApplicationConfiguration applicationConfiguration;

        public Bootstrapper(ApplicationConfiguration applicationConfiguration)
        {
            this.applicationConfiguration = applicationConfiguration;
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            pipelines.BeforeRequest += ctx =>
            {
                if (!string.IsNullOrEmpty(ctx.Request.Headers.Authorization))
                {
                    var authHeader = new AuthorizationHeader(ctx.Request.Headers.Authorization);
                    if (authHeader.Scheme.ToLower() == "basic")
                    {
                        var creds = new Credentials(authHeader.Parameter);
                        var accountRepository = container.Resolve<AccountRepository>();
                        var account = accountRepository.FindByUsername(creds.Username).GetAwaiter().GetResult();

                        if (account != null)
                        {
                            var hashingService = container.Resolve<HashingService>();
                            if (hashingService.ValidatePassword(creds.Password, account.Hash))
                            {
                                var claims = new List<Claim>
                                {
                                    new Claim("AccountId", account.Id.ToString()),
                                    new Claim("Username", account.UserName),
                                    new Claim("Email", account.Email)
                                };

                                var identity = new ClaimsIdentity(claims, "Basic");
                                var principle = new ClaimsPrincipal(identity);
                                ctx.CurrentUser = principle;
                            }
                        }
                    }
                }

                return null;
            };
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

            container.Register(applicationConfiguration.Database);
            container.Register(applicationConfiguration.Email);

            container.Register<IDbConnectionProvider, PostgresConnectionProvider>();

            container.Register<AccountRepository>();

            container.Register<EmailService>();
            container.Register<HashingService>();

            container.Register<IRequestHandler<SignUpCommand, Unit>, SignUpCommandHandler>();
            container.Register<IRequestHandler<AddPostCommand, Guid>, AddPostCommandHandler>();

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