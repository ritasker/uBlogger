using MediatR;
using uBlogger.Infrastructure.Posts;

namespace uBlogger.Api.Features.Users.Post
{
    public class AddPostCommandHandler : IRequestHandler<AddPostCommand>
    {
        private readonly PostRepository _postRepository;

        public AddPostCommandHandler(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public void Handle(AddPostCommand message)
        {
            _postRepository.Save(message.Id, message.Username, message.Content);
        }
    }
}