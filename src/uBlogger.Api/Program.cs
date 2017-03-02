using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using uBlogger.Infrastructure;

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
                .SetBasePath(Path.GetDirectoryName(typeof(Startup).GetTypeInfo().Assembly.Location))
                .AddEnvironmentVariables();                ;

            var config = builder.Build();

            var appConfig = new ApplicationConfiguration(config["ConnectionString"]);

            using (var bootstrapper = new Bootstrapper(appConfig))
            {
                using (var host = new WebHostBuilder()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseKestrel()
                    .UseIISIntegration()
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
    }
}