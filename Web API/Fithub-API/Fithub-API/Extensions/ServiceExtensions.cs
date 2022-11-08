using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Fithub_API.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// configuration of jwt default options
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureJWTDefaults(this IServiceCollection services, IConfiguration configuration) =>

          services.AddAuthentication(option =>
          {
              option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

          })
          .AddJwtBearer(opt =>
          {
              opt.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,


                  ValidIssuer = configuration.GetSection("JWTSettings:validIssuer").Value,//   GetChildren().Select(x => x.Key.Equals("validIssuer")).ToString(),
                  ValidAudience = configuration.GetSection("JWTSettings:validAudience").Value,// .GetChildren().Select(x => x.Key.Equals("validAudience")).ToString(),
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWTSettings:securityKey").Value))//.GetChildren().Select(x => x.Key.Equals("securityKey")).ToString()))

              };
          });

        /// <summary>
        /// configuration of cors
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCORS(this IServiceCollection services) =>

            services.AddCors(option =>
            {
                option.AddPolicy("EnableCORS", builder =>
            {
                builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
            });
            });

    }


}
