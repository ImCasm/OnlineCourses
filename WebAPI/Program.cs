using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistencia;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostServer = CreateHostBuilder(args).Build();

            using(var environment = hostServer.Services.CreateScope())
            {
                var services = environment.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<CoursesContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var loggin = services.GetRequiredService<ILogger<Program>>();
                    loggin.LogError(ex, "Error en la migracion");
                }
            }
            hostServer.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
