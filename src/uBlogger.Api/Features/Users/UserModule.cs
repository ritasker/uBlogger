using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Nancy.Validation;
using uBlogger.Api.Features.Users.Follow;

namespace uBlogger.Api.Features.Users
{
    public class UserModule : NancyModule
    {
        private readonly IMediator mediator;

        public UserModule(IMediator mediator) : base("/Users")
        {
            this.mediator = mediator;

            Post("/{Username}/Follow", async _ => await FollowUser());

        }

        private async Task<object> FollowUser()
        {
            this.RequiresAuthentication();

            var model = this.BindAndValidate<FollowUserViewModel>();

            if (ModelValidationResult.IsValid)
            {
                var username = Context.CurrentUser.Claims.First(x => x.Type == "Username").Value;

                if (model.Username.ToLower() != username.ToLower())
                {
                    var accountId = Context.CurrentUser.Claims.First(x => x.Type == "AccountId").Value;
                    await mediator.Send(new FollowUserCommand(Guid.Parse(accountId), model.Username));

                    return HttpStatusCode.Created;
                }

                ModelValidationResult.Errors.Add("Username",
                    new List<ModelValidationError>
                    {
                        new ModelValidationError("Username", "You cannot Follow Yourself")
                    });
            }

            return Negotiate
                .WithModel(ModelValidationResult.FormattedErrors)
                .WithStatusCode(HttpStatusCode.UnprocessableEntity);
        }
    }
}