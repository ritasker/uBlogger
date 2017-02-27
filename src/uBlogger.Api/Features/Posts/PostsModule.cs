using System;
using System.Linq;
using MediatR;
using Nancy;
using Nancy.Security;
using Nancy.ModelBinding;
using uBlogger.Api.Features.Posts.AddPost;
using uBlogger.Api.Features.Posts.UserPosts;

namespace uBlogger.Api.Features.Posts
{
    public class PostsModule : NancyModule
    {
        private readonly IMediator mediator;

        public PostsModule(IMediator mediator) : base("/{Username}/Posts")
        {
            this.mediator = mediator;

            Post("/", args => AddPost(mediator));

            Get("/", async args =>
            {
                var result = await this.mediator.Send(new UserPostsQuery(args.Username));
                return Negotiate
                    .WithModel(result)
                    .WithStatusCode(HttpStatusCode.OK);
            });


        }

        private object AddPost(IMediator mediator)
        {
            this.RequiresAuthentication();

            var model = this.BindAndValidate<AddPostViewModel>();

            if (ModelValidationResult.IsValid)
            {
                var username = Context.CurrentUser.Claims.First(x => x.Type == "Username").Value;

                if (model.Username == username)
                {
                    var accountId = Context.CurrentUser.Claims.First(x => x.Type == "AccountId").Value;

                    var postId = mediator.Send(new AddPostCommand(Guid.Parse(accountId), model.Content))
                        .GetAwaiter()
                        .GetResult();
                    return Negotiate
                        .WithStatusCode(HttpStatusCode.Created)
                        .WithHeader("location", $"http://localhost:5000/Accounts/{model.Username}/Posts/{postId}");
                }

                return HttpStatusCode.Forbidden;
            }

            return Negotiate
                .WithModel(ModelValidationResult.FormattedErrors)
                .WithStatusCode(HttpStatusCode.UnprocessableEntity);
        }
    }
}
