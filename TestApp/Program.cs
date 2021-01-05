using AD.DAL.Services;
using AD.DAL.Services.Base;
using AD.DAL.Services.Interfaces;
using AD.Domain.Models;
using AD.Domain.Profiles;
using AD.Domain.Settings.Options;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.DirectoryServices.AccountManagement;
using System.IO;

namespace TestApp
{

    class Program
    {
        static void Main(string[] args)
        {
            var config = BuildConfig();

            var host = BuildHost(config);

            Log.Logger.Information("Application starting");

            var adService = ActivatorUtilities.CreateInstance<AdService>(host.Services);

            var domainUsers = adService.GetDomainUsers();

            Log.Logger.Information("Application finish");
        }

        private static void TestAutoMapper() {
            IServiceCollection services = new ServiceCollection();

            services.AddAutoMapper(typeof(AdUser));
            var provider = services.BuildServiceProvider();
            using (var scope = provider.CreateScope())
            {
                var mapper = scope.ServiceProvider.GetService<IMapper>();

                foreach (var typeMap in mapper.ConfigurationProvider.GetAllTypeMaps())
                {
                    Console.WriteLine($"{typeMap.SourceType.Name} -> {typeMap.DestinationType.Name}");
                }

                foreach (var service in services)
                {
                    Console.WriteLine(service.ServiceType + " - " + service.ImplementationType);
                }
            }
        }

        private static IHost BuildHost(IConfigurationRoot configuration) {

            return Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        services.AddScoped<LoggingService>();
                        services.Configure<AdOptions>(configuration.GetSection(AdOptions.AD));

                        services.AddAutoMapper(typeof(AdUserProfile));
                        services.AddScoped<IAdService, AdService>();
                    })
                    .UseSerilog()
                    .Build();
        }

        private static IConfigurationRoot BuildConfig() {
            var builder = new ConfigurationBuilder();

            var config = builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: true, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            return config;
        }
    }
}
