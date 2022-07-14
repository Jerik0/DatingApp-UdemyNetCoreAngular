### To start application:
`dotnet watch run`

### URL for application:
`https://localhost:5001`

----------
### Entity Framework And Database
**#1. Install Required Nuget Packages**

NOTE: *Use NuGet Gallery extension for VS Code*

Package #1: `Microsoft.EntityFrameworkCore`

Package #2: `Microsoft.EntityFrameworkCore.Design`

Package #3: `Pomelo.EntityFrameworkCore.MySql`

Package #4: `Pomelo.JsonObject`

Package #5: `Pomelo.EntityFrameworkCore.MySql.Json.Microsoft`

----------
**#2. Set Up Entity Framework**
* Create a class with desired properties
```
public class AppUser
{
    public int Id { get; set; }
    public string UserName { get; set; }
}
```
Note: *The particular fields shown above must be named **EXACTLY** how they appear to work with Entity Framework*

* Create a class that inherits from DbContext
```
public class DataContext : DbContext
{
  public DataContext(DbContextOptions options) : base(options)
  {
  }

  public DbSet<AppUser> User { get; set; }
}
```

* Create connection string and put in appsettings.Development.json:
```json
"ConnectionStrings": {
  "DefaultConnection": "server=127.0.0.1;uid=root;pwd=Jjc01dec!;database=test"
},
```

* Add new service in Startup.cs:
<pre>
public void ConfigureServices(IServiceCollection services)
{
    <b>services.AddDbContext<DataContext>(options => {
      options.UseMySql(
        _config.GetConnectionString("DefaultConnection"), 
        new MySqlServerVersion(new Version(8, 0, 11))
      );
    });</b>
    services.AddControllers();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
    });
}
</pre>

* Create migrations into folder Data/Migrations (or wherever):
```
dotnet ef migrations add InitialCreate -o Data/Migrations
```

* Actually update your database now:
```
dotnet ef database update
```

CONGRATS!! 🥳 You have successfully hooked up Entity Framework!