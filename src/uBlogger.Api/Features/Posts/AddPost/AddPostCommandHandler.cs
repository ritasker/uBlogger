using System;
using MediatR;
using uBlogger.Domain.Entities;
using uBlogger.Infrastructure.Posts;

namespace uBlogger.Api.Features.Posts
{
    public class AddPostCommandHandler : IRequestHandler<AddPostCommand, Guid>
    {
        private readonly PostRepository _postRepository;

        public AddPostCommandHandler(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public Guid Handle(AddPostCommand message)
        {
            var post = new Post(message.AccountId, message.Content);
            _postRepository.Save(post);
            return post.Id;
        }
    }
}