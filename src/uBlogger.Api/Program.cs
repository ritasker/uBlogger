using System;
using System.Linq;
using System.Reflection;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using uBlogger.Infrastructure;
using uBlogger.Infrastructure.Email;

namespace uBlogger.Api
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("apisettings.json")
                .SetBasePath(Path.GetDirectoryName(typeof(Startup).GetTypeInfo().Assembly.Location));

            var config = builder.Build();

            var appConfig = GetApplicationConfiguration(config["ConnectionString"]);

            using (var bootstrapper = new Bootstrapper(appConfig))
            {
                using (var host = new WebHostBuilder()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseKestrel()
                    .ConfigureServices(s =>
                    {
                        s.AddSingleton(bootstrapper);
                    })
                    .UseStartup<Startup>()
                    .Build())
                {
                    host.Start();
                    Console.WriteLine("Running on http://localhost:5000");

                    var appLifeTime = host.Services.GetRequiredService<IApplicationLifetime>();
                    appLifeTime.ApplicationStopping.Register(() =>
                    {
                        Console.WriteLine("Stopping application...");
                    });

                    Console.CancelKeyPress += (sender, e) =>
                    {
                        appLifeTime.StopApplication();
                        e.Cancel = true;
                    };

                    appLifeTime.ApplicationStopping.WaitHandle.WaitOne();
                }
            }
        }

        private static ApplicationConfiguration GetApplicationConfiguration(string connectionString)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var sql = "SELECT * FROM settings.\"EmailConfig\";";

                var emailConfig = connection.Query<EmailConfiguation>(sql).FirstOrDefault();

                return new ApplicationConfiguration(emailConfig, connectionString);
            }
        }
    }
}