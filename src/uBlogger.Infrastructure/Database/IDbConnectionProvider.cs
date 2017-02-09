namespace uBlogger.Infrastructure.Database
{
    using System.Data;

    public interface IDbConnectionProvider
    {
        IDbConnection GetConnection();
    }
}