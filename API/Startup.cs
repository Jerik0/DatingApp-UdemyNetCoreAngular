using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
          _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          // using AddScoped instead of Singleton, which would continually use resources.
          // in this case it just needs to create a token and won't need to be active after.
          // AddTransient means service is created and destroyed as soon as the method is finished. 
          // AddScoped is considered correct for http requests.
          services.AddScoped<ITokenService, TokenService>();

          services.AddDbContext<DataContext>(options => {
            options.UseMySql(
              _config.GetConnectionString("DefaultConnection"), 
              new MySqlServerVersion(new Version(8, 0, 11))
            );
          });
          services.AddControllers();
          services.AddSwaggerGen(c =>
          {
              c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
          });
          services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          if (env.IsDevelopment())
          {
              app.UseDeveloperExceptionPage();
              app.UseSwagger();
              app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
          }

          app.UseHttpsRedirection();

          app.UseRouting();

          app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

          app.UseAuthorization();

          app.UseEndpoints(endpoints =>
          {
              endpoints.MapControllers();
          });
        }
    }
}
