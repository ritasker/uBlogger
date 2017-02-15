using System;
using System.Collections.Generic;
using System.Data;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Nancy;
using Nancy.Testing;
using uBlogger.Api.Features.Accounts;
using uBlogger.Api.Features.Accounts.SignUp;
using uBlogger.Infrastructure.Accounts;
using uBlogger.Infrastructure.Email;
using uBlogger.Tests.Unit.Helpers;
using Xunit;

namespace uBlogger.Tests.Unit.Accounts
{
    public class AccountsTests
    {
        [Fact]
        public void ShouldSignUpANewAccount()
        {
//            var dbConnectionProvider = A.Fake<IDbConnectionProvider>();
//            A.CallTo(() => dbConnectionProvider.GetConnection()).Returns(A.Fake<IDbConnection>());

            var commandHandler = new SignUpCommandHandler(new AccountRepository(null), new EmailService(null));
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