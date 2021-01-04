using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using VoiceOfKarabakh.Infrastructure.Data;

namespace VoiceOfKarabakh.UI.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();

            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    ILogger logger = services.GetRequiredService<ILogger>();
                    logger.LogError(ex.Message);
                }
            }

            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(l =>
                {
                    l.AddConsole();
                });
    }
}
