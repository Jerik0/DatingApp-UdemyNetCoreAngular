using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
  // must be *static*. That means that we do not need a new instance of this class in order to use it.
  public static class IdentityServiceExtensions
  {
      public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
      {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options => 
          {
            options.TokenValidationParameters = new TokenValidationParameters
            {
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
              ValidateIssuer = false, // the issuer is this API server
              ValidateAudience = false // Audience in this case will be the angular application
            };
          });

        return services;
      }
  }
}