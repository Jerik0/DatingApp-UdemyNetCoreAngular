### To start application:
`dotnet watch run`

### URL for application:
`https://localhost:5001`

# Entity Framework And Database
## #1. Install Required Nuget Packages

NOTE: *Use NuGet Gallery extension for VS Code*

Package #1: `Microsoft.EntityFrameworkCore`

Package #2: `Microsoft.EntityFrameworkCore.Design`

Package #3: `Pomelo.EntityFrameworkCore.MySql`

Package #4: `Pomelo.JsonObject`

Package #5: `Pomelo.EntityFrameworkCore.MySql.Json.Microsoft`

----------
## #2. Set Up Entity Framework
**Make sure that Startup.cs has a readonly config variable and a constructor to assign it**

```
private readonly IConfiguration _config;

public Startup(IConfiguration config)
{
  _config = config;
}
```

**Create an Entity (just a class) with desired properties:**

(this will represent a row in the table defined in DbContext)
```
public class AppUser
{
    public int Id { get; set; }
    public string UserName { get; set; }
}
```
Note: *The particular fields shown above must be named **EXACTLY** how they appear in order to work with Entity Framework*

**Create a class that inherits from DbContext:**
```
public class DataContext : DbContext
{
  public DataContext(DbContextOptions options) : base(options)
  {
  }

  public DbSet<AppUser> Users { get; set; } // this will represent the table name
}
```
**
**Create connection string and put in appsettings.Development.json:**
```json
"ConnectionStrings": {
  "DefaultConnection": "server=127.0.0.1;uid=root;pwd=Jjc01dec!;database=test"
},
```

**Add new service in Startup.cs:**
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

**Create migrations into folder Data/Migrations (or wherever):**

`dotnet ef migrations add InitialCreate -o Data/Migrations`

**Actually update your database now:**

`dotnet ef database update`

CONGRATS!! 🥳 You have successfully hooked up Entity Framework!

----------

## #3. Set up SQL Tools to perform CRUD operations to database

**#1. Install SQLTools Extension**

*This will be the extension that interacts with your database using queries*

**#2. Install SQLTools MySQL/MariaDB Extension**

*This will be the driver that establishes a connection to your database, and the connection that SQLTools will use for the queries* 

**#3. Set up MySql Connection**

* Press `CTRL+Shift+P` and search for Add New Connection.
* Click on SQL Tools Management: Add New Connection
* Click on `MySQL`
* In the new page that pops up, fill in your SQL database information
* Be sure to test the connection to the database and then save it.

* **NOTE: IF USING MYSQL 8+, YOU MUST AUTHENTICATE USING FOLLOWING METHOD:**

In your MySQL connection, type the following commands.
```
CREATE USER 'someusername'@'localhost'IDENTIFIED WITH mysql_native_password BY 'password';
```
```
GRANT ALL PRIVILEGES ON *.* TO 'someusername'@'%';
```
```
FLUSH PRIVILEGES;
```
* **Make sure to edit the connection with SQL Tools Management with this new user's credentials**

* ***RESTART VS CODE*** and you should be good to start interacting with the DB using queries!

## Let's test it.

* Using `CTRL+SHIFT+P`, type `Connect` and click on `SQL Tools: Connect`.
* Click on the connection that you set up earlier.
* In the body of the file that opened, type a query.
* Highlight the query and type `CTRL+SHIFT+P`
* Click `Run Current Query` from SQL Tools. You should have a successful query!!

## Useful Links

[SQL Tools Documentation](https://vscode-sqltools.mteixeira.dev/driver/mysql#1-connections)

# Section 2: Controllers

# Section 3: Authentication and Logging In
