using FluentAssertions;
using Nancy;
using Nancy.Testing;
using Xunit;
using uBlogger.Api;

namespace uBlogger.Tests.Unit
{
    public class SignUpTests
    {
        [Fact]
        public void ShouldBeAbleToSignUpAUser()
        {
            var browser = new Browser(new Bootstrapper());

            var result = browser.Post("/SignUp", with =>
            {
                with.HttpRequest();
            });

            result.Result.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}