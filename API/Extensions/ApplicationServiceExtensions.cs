
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
  // must be *static*. That means that we do not need a new instance of this class in order to use it.
  public static class ApplicationServiceExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
      // using AddScoped instead of Singleton, which would continually use resources.
      // in this case it just needs to create a token and won't need to be active after.
      // AddTransient means service is created and destroyed as soon as the method is finished. 
      // AddScoped is considered correct for http requests.
      services.AddScoped<ITokenService, TokenService>();

      services.AddDbContext<DataContext>(options => 
      {
        options.UseMySql(
          config.GetConnectionString("DefaultConnection"), 
          new MySqlServerVersion(new Version(8, 0, 11))
        );
      });

      return services;
    }
  }
}