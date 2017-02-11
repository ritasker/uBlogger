using System.Threading.Tasks;
using MediatR;
using Nancy;
using Nancy.ModelBinding;
using uBlogger.Api.Features.Accounts.SignUp;

namespace uBlogger.Api.Features.Accounts
{
    public class AccountModule : NancyModule
    {
        private readonly IMediator _mediator;

        public AccountModule(IMediator mediator)
        {
            _mediator = mediator;

            Post("/SignUp", async _ => await SignUp());
        }

        private Task<HttpStatusCode> SignUp()
        {
            var model = this.Bind<SignUpViewModel>();
            var command = new SignUpCommand(model.Name, model.Email);

            _mediator.Send(command);

            return Task.FromResult(HttpStatusCode.Created);
        }
    }
}