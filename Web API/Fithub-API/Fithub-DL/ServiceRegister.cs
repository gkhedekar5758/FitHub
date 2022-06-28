using Fithub_DL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_DL
{
  /// <summary>
  /// class to register the services
  /// </summary>
  public static class ServiceRegister
  {
    /// <summary>
    /// this method will be called via api
    /// </summary>
    /// <param name="services">service from API's Startup class</param>
    public static void AddDLServices(this IServiceCollection services)
    {
      services.AddScoped<IReadUser, ReadUser>();
      services.AddScoped<IWriteUser, WriteUser>();
      services.AddScoped<IReadClass, ReadClass>();
      services.AddScoped<IReadCoach, ReadCoach>();
      services.AddScoped<IReadTestimony, ReadTestimony>();
      services.AddScoped<IWriteCoach, WriteCoach>();
    }
  }
}
