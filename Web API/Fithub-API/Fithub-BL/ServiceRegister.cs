using Fithub_BL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Fithub_BL
{
    /// <summary>
    /// class to register the services
    /// </summary>
    public static class ServiceRegister
    {
        /// <summary>
        /// this method will be called via api
        /// </summary>
        /// <param name="services">service from API's start up class</param>
        public static void AddBLServices(this IServiceCollection services)
        {
            services.AddScoped<IQueryUser, QueryUser>();
            services.AddScoped<IUpdateUser, UpdateUser>();
            services.AddScoped<IQueryClass, QueryClasss>();
            services.AddScoped<IQueryCoach, QueryCoach>();
            services.AddScoped<IQueryTestimony, QueryTestimony>();
            services.AddScoped<IUpdateCoach, UpdateCoach>();
            services.AddScoped<IUpdateTestimony, UpdateTestimony>();
        }
    }
}
