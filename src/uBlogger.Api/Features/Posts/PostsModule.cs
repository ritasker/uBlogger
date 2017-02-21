using System;
using System.Linq;
using MediatR;
using Nancy;
using Nancy.Security;
using Nancy.ModelBinding;

namespace uBlogger.Api.Features.Posts
{
    public class PostsModule : NancyModule
    {
        public PostsModule(IMediator mediator) : base("/Accounts/{Username}/Posts")
        {
            Post("/", args =>
            {
                this.RequiresAuthentication();

                var model = this.BindAndValidate<AddPostViewModel>();

                if (ModelValidationResult.IsValid)
                {
                    var username = Context.CurrentUser.Claims.First(x => x.Type == "Username").Value;

                    if (model.Username == username)
                    {
                        var accountId = Context.CurrentUser.Claims.First(x => x.Type == "AccountId").Value;

                        var postId = mediator.Send(new AddPostCommand(Guid.Parse(accountId), model.Content)).GetAwaiter().GetResult();
                        return Negotiate
                            .WithStatusCode(HttpStatusCode.Created)
                            .WithHeader("location", $"http://localhost:5000/Accounts/{model.Username}/Posts/{postId}");
                    }

                    return HttpStatusCode.Forbidden;
                }

                return Negotiate
                    .WithModel(ModelValidationResult.FormattedErrors)
                    .WithStatusCode(HttpStatusCode.UnprocessableEntity);
            });
        }
    }
}
