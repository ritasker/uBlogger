using System.Threading.Tasks;
using MediatR;
using Nancy;
using Nancy.ModelBinding;
using uBlogger.Api.Features.Accounts.SignUp;
using uBlogger.Api.Features.Accounts.ViewModels;

namespace uBlogger.Api.Features.Accounts
{
    public class AccountModule : NancyModule
    {
        private readonly IMediator _mediator;

        public AccountModule(IMediator mediator)
        {
            _mediator = mediator;
            Post("/SignUp", async _ => await SignUp());
            Post("/SignIn", async _ => await SignIn());
        }

        private Task<HttpStatusCode> SignUp()
        {
            var model = this.BindAndValidate<SignUpViewModel>();
            var command = new SignUpCommand(model.Name, model.Email);

            return Task.FromResult(HttpStatusCode.Created);
        }

        private async Task<int> SignIn()
        {
            var model = this.BindAndValidate<SignIn>();
            return 200;
        }
    }
}