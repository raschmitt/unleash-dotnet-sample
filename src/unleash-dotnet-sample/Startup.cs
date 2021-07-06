using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Unleash;

namespace unleash_dotnet_sample
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHealthChecks();
            
            services.AddSingleton<IUnleash>(c => new DefaultUnleash(new UnleashSettings()
            {
                UnleashApi = new Uri("http://web:4242/api"),
                AppName = "sample",
                CustomHttpHeaders = new Dictionary<string, string>()
                {
                    {"Authorization","143c0a07001c6ef6b8c1ff3d5ed26d66f2a4440f00d3130a1f84b7de44f3616f" }
                }
            }));
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "unleash_dotnet_sample", Version = "v1" });
            });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "unleash-dotnet-sample v1"));

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}