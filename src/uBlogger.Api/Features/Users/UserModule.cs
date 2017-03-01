using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Nancy.Validation;
using uBlogger.Api.Features.Posts.Timeline;
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

            Get("/{Timeline}", async _ =>
            {
                this.RequiresAuthentication();
                var username = Context.CurrentUser.Claims.First(x => x.Type == "Username").Value;
                var result =  await this.mediator.Send(new UserTimelineQuery(username));

                return Negotiate
                    .WithModel(result)
                    .WithStatusCode(HttpStatusCode.OK);
            });

        }

        private async Task<object> FollowUser()
        {
            this.RequiresAuthentication();

            var model = this.BindAndValidate<FollowUserViewModel>();

            if (ModelValidationResult.IsValid)
            {
                var username = Context.CurrentUser.Claims.First(x => x.Type == "Username").Value;

                if (!String.Equals(model.Username, username, StringComparison.CurrentCultureIgnoreCase))
                {
                    await mediator.Send(new FollowUserCommand(username, model.Username));
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