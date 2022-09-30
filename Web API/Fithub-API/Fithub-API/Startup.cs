using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Fithub_API.Extensions;
using Fithub_API.JWTFeature;
using Fithub_BL;
using Fithub_DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fithub_API.Helper;

namespace Fithub_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //SERVICE PIPELINE
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureJWTDefaults(Configuration);
            services.ConfigureCORS();
            services.AddControllers();
            services.AddScoped<JWTHelper>();
            services.AddSingleton<IFithubConfigHelper, FithubConfigHelper>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fithub_API", Version = "v1" });
            });
            services.AddBLServices();
            services.AddDLServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //MIDDLEWARE PIPELINE
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fithub_API v1"));
            }

            app.UseRouting();
            app.UseCors("EnableCORS");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
