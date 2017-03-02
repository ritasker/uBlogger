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
using uBlogger.Api.Features.Users.Post;
using uBlogger.Api.Features.Users.Timeline;
using uBlogger.Api.Features.Users.UserPosts;

namespace uBlogger.Api.Features.Users
{
    public class UserModule : NancyModule
    {
        private readonly IMediator mediator;

        public UserModule(IMediator mediator) : base("/Users")
        {
            this.mediator = mediator;

            Post("/{Username}/Follow", async _ => await FollowUser());

            Post("/{Username}/Posts", async args => await AddPost());

            Get("/{Username}/Posts", async args => await UserPosts(args));

            Get("/{Username}/Timeline", async args => await UserTimeline(args));

        }

        private async Task<object> UserTimeline(dynamic args)
        {
            this.RequiresAuthentication();
            var username = Context.CurrentUser.Claims.First(x => x.Type == "Username").Value;

            if (args.Username != username)
                return HttpStatusCode.Forbidden;

            var result = await mediator.Send(new UserTimelineQuery(username));
            return Negotiate
                .WithModel(result)
                .WithStatusCode(HttpStatusCode.OK);
        }

        private async Task<object> UserPosts(dynamic args)
        {
            var result = await mediator.Send(new UserPostsQuery(args.Username));
            return Negotiate
                .WithModel(result)
                .WithStatusCode(HttpStatusCode.OK);
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

        private async Task<object> AddPost()
        {
            this.RequiresAuthentication();

            var model = this.BindAndValidate<AddPostViewModel>();

            if (ModelValidationResult.IsValid)
            {
                var username = Context.CurrentUser.Claims.First(x => x.Type == "Username").Value;

                if (model.Username != username)
                    return HttpStatusCode.Forbidden;

                await mediator.Send(new AddPostCommand(Guid.NewGuid(), username, model.Content));
                return Negotiate
                    .WithStatusCode(HttpStatusCode.Created);
            }

            return Negotiate
                .WithModel(ModelValidationResult.FormattedErrors)
                .WithStatusCode(HttpStatusCode.UnprocessableEntity);
        }
    }
}