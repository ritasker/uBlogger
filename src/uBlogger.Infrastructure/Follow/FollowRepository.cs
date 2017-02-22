using System.Threading.Tasks;
using uBlogger.Infrastructure.Database;
using uBlogger.Infrastructure.Follow.DbCommands;

namespace uBlogger.Infrastructure.Follow
{
    public class FollowRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public FollowRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }
        
        public async Task Save(Domain.Entities.Follow follow)
        {
            using (var connection = _dbConnectionProvider.GetConnection())
            {
                var command = new SaveFollowCommand(follow);
                await command.ExecuteAsync(connection);
            }
        }
    }
}