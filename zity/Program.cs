using Microsoft.EntityFrameworkCore;
using ZiTy.Data;
using DotNetEnv;
using ZiTy.Repositories.Interfaces;
using ZiTy.Repositories.Implementations;
using ZiTy.Services.Interfaces;
using ZiTy.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

// Retrieve environment variables
var MySQLServer = Environment.GetEnvironmentVariable("MYSQL_SERVER") ?? throw new ArgumentException("MYSQL_SERVER is missing.");
var MySQLPort = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? throw new ArgumentException("MYSQL_PORT is missing.");
var MySQLlDatabase = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? throw new ArgumentException("MYSQL_DATABASE is missing.");
var MySQLUser = Environment.GetEnvironmentVariable("MYSQL_USER") ?? throw new ArgumentException("MYSQL_USER is missing.");
var MySQLlPassword = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? throw new ArgumentException("MYSQL_PASSWORD is missing.");

// Convert mysqlPort to integer
if (!int.TryParse(MySQLPort, out int port))
{
    throw new ArgumentException($"Invalid value '{MySQLPort}' for 'MYSQL_PORT'.");
}

var connectionString = $"Server={MySQLServer};Port={port};Database={MySQLlDatabase};User={MySQLUser};Password={MySQLlPassword}";

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext with MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add logging service
builder.Services.AddLogging();

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register services
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Check connection to the MySQL database
var logger = app.Services.GetRequiredService<ILogger<Program>>();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        if (dbContext.Database.CanConnect())
        {
            logger.LogInformation("Database connection successful!");
        }
        else
        {
            logger.LogError("Database connection failed.");
            // Exit the application with a non-zero status code
            Environment.Exit(1);
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while trying to connect to the database.");
        Environment.Exit(1);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
