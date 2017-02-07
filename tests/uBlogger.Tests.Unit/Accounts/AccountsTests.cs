using FluentAssertions;
using Nancy;
using Nancy.Testing;
using uBlogger.Api.Features.Accounts;
using uBlogger.Api.Features.Accounts.ViewModels;
using uBlogger.Tests.Unit.Helpers;
using Xunit;

namespace uBlogger.Tests.Unit.Accounts
{
    public class AccountsTests
    {
        [Fact]
        public void ShouldSignUpANewAccount()
        {
            var browser = new Browser(with =>
            {
                with.Module<AccountModule>();
            });

            var model = new SignUp
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