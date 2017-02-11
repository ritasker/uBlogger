using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            var apiConfiguration = new ApiConfiguration();
            config.Bind(apiConfiguration);

            using (var bootstrapper = new Bootstrapper(apiConfiguration))
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

                    var appLifeTime = host.Services.GetRequiredService<IApplicationLifetime>();

                    appLifeTime.ApplicationStopping.Register(() =>
                    {
                        Console.WriteLine("Stopping application, please wait for confirmation...");
                    });

                    appLifeTime.ApplicationStopped.Register(() =>
                    {
                        Console.WriteLine("Web Server has stopped");
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