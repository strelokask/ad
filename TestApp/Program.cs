using AD.DAL.Services;
using AD.DAL.Services.Base;
using AD.DAL.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;

namespace TestApp
{

    class Program
    {
        static void Main(string[] args)
        {
            BuildConfig();

            var host = BuildHost();

            Log.Logger.Information("Application starting");

            var adService = ActivatorUtilities.CreateInstance<AdService>(host.Services);

            adService.GetAdUsers();

            Log.Logger.Information("Application finish");
        }

        private static IHost BuildHost() {

            return Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        services.AddSingleton<LoggingService>();
                        services.AddScoped<IAdService, AdService>();
                    })
                    .UseSerilog()
                    .Build();
        }

        private static void BuildConfig() {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: true, reloadOnChange: true)
                ;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}
