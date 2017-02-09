using System;
using System.Collections.Generic;
using FluentAssertions;
using MediatR;
using Nancy;
using Nancy.Testing;
using uBlogger.Api.Features.Accounts;
using uBlogger.Api.Features.Accounts.SignUp;
using uBlogger.Tests.Unit.Helpers;
using Xunit;

namespace uBlogger.Tests.Unit.Accounts
{
    public class AccountsTests
    {
        [Fact]
        public void ShouldSignUpANewAccount()
        {
            var commandHandler = new SignUpCommandHandler();
            var browser = new Browser(with =>
            {
                with.Module<AccountModule>();
                with.Dependency<IMediator>(typeof(Mediator));
                with.Mediatr(new Dictionary<Type, object>
                {
                    [typeof(IRequestHandler<SignUpCommand, MediatR.Unit>)] = commandHandler
                });
            });

            var model = new SignUpViewModel
            {
                Name = "Rich Tasker",
                Email = "richard.tasker@example.com"
            }.ToJsonString();

            var response = browser.Post("/SignUp", with =>
            {
                with.HttpRequest();
                with.Body(model);
            });

            response.Result.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}