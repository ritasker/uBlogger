using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using uBlogger.Api.Features.Accounts.SignUp;
using uBlogger.Infrastructure.MessageBus;
using uBlogger.Infrastructure.Security;

namespace uBlogger.Api.Features.Accounts
{
    public class AccountModule : NancyModule
    {
        private readonly ServiceBusClient busClient;
        private readonly HashingService hashingService;

        public AccountModule(ServiceBusClient busClient, HashingService hashingService)
        {
            this.busClient = busClient;
            this.hashingService = hashingService;

            Post("/SignUp", async _ => await SignUp());
        }

        private async Task<object> SignUp()
        {
            var model = this.BindAndValidate<SignUpViewModel>();

            if (!ModelValidationResult.IsValid)
                return Negotiate
                    .WithModel(ModelValidationResult.FormattedErrors)
                    .WithStatusCode(HttpStatusCode.UnprocessableEntity);

            var command = new SignUpCommand(
                model.Username,
                model.Email,
                hashingService.HashPassword(model.Password)
            );

            await busClient.Send(command);

            return HttpStatusCode.Accepted;
        }
    }
}