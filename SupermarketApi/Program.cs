namespace SupermarketApi
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using SupermarketApi.Data;

    [ExcludeFromCodeCoverage]
    public sealed class Program
    {
        [SuppressMessage("Globalization", "CA1303: Do not pass literals as localized parameters", Justification = "Library is not localized.")]
        [SuppressMessage("Design", "CA1031: Do not catch general exception types", Justification = "For now, unknown exceptions.")]
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var contex = services.GetRequiredService<StoreContext>();
                    await contex.Database.MigrateAsync().ConfigureAwait(false);
                    await StoreContextSeed.SeedAsync(contex, loggerFactory).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occured during the migration.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    _ = webBuilder.UseStartup<Startup>();
                });
        }
    }
}
