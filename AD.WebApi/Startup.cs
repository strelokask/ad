using AD.DAL.Services;
using AD.DAL.Services.Base;
using AD.DAL.Services.Interfaces;
using AD.Domain.Profiles;
using AD.Domain.Settings.Options;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AD.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton(x => x.GetService<ILoggerFactory>().CreateLogger(this.GetType().Namespace));
            services.Configure<AdOptions>(Configuration.GetSection(AdOptions.AD));

            services.AddAutoMapper(typeof(AdUserProfile));
            services.AddScoped<IAdService, AdService>();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v.1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "AD.Api", Version = "1.0" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
