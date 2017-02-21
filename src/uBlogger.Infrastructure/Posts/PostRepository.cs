﻿using System.Threading.Tasks;
using uBlogger.Domain.Entities;
using uBlogger.Infrastructure.Database;
using uBlogger.Infrastructure.Posts.DbCommands;

namespace uBlogger.Infrastructure.Posts
{
    public class PostRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public PostRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public async Task Save(Post post)
        {
            using (var connection = _dbConnectionProvider.GetConnection())
            {
                var command = new SavePostCommand(post);
                await command.ExecuteAsync(connection);
            }
        }
    }
}