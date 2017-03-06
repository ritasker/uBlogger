using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Nancy.Validation;
using uBlogger.Api.Features.Users.Follow;
using uBlogger.Api.Features.Users.Post;
using uBlogger.Infrastructure.MessageBus;
using uBlogger.Infrastructure.Posts;

namespace uBlogger.Api.Features.Users
{
    public class UserModule : NancyModule
    {
        private readonly ServiceBusClient serviceBusClient;
        private readonly PostRepository repository;

        public UserModule(ServiceBusClient serviceBusClient, PostRepository repository) : base("/Users")
        {
            this.repository = repository;
            this.serviceBusClient = serviceBusClient;

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

            var result = await repository.UserTimeline(username);

            var posts = result.Select(x => new PostViewModel
            {
                Id = x.RowKey,
                Date = x.Timestamp.UtcDateTime,
                Author = x.PartitionKey,
                Content = x.Content
            });

            return Negotiate
                .WithModel(posts)
                .WithStatusCode(HttpStatusCode.OK);
        }

        private async Task<object> UserPosts(dynamic args)
        {
            string username = args.Username;
            var result = await repository.PostsByUser(username);

            var posts = result.Select(x => new PostViewModel
            {
                Id = x.RowKey,
                Date = x.Timestamp.UtcDateTime,
                Author = x.PartitionKey,
                Content = x.Content
            });

            return Negotiate
                .WithModel(posts)
                .WithStatusCode(HttpStatusCode.OK);
        }

        private async Task<object> FollowUser()
        {
            this.RequiresAuthentication();

            var model = this.BindAndValidate<FollowUserViewModel>();

            if (ModelValidationResult.IsValid)
            {
                var username = Context.CurrentUser.Claims.First(x => x.Type == "Username").Value;

                if (!string.Equals(model.Username, username, StringComparison.CurrentCultureIgnoreCase))
                {
                    await serviceBusClient.Send(new FollowUserCommand(username, model.Username));
                    return HttpStatusCode.Accepted;
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

            if (!ModelValidationResult.IsValid)
                return Negotiate
                    .WithModel(ModelValidationResult.FormattedErrors)
                    .WithStatusCode(HttpStatusCode.UnprocessableEntity);

            var username = Context.CurrentUser.Claims.First(x => x.Type == "Username").Value;
            if (!string.Equals(model.Username, username, StringComparison.CurrentCultureIgnoreCase))
                return HttpStatusCode.Forbidden;

            await serviceBusClient.Send(new PostContentCommand(Guid.NewGuid(), username, model.Content));
            return Negotiate
                .WithStatusCode(HttpStatusCode.Accepted);
        }
    }
}