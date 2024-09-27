using DotNetEnv;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZiTy.Data;
using ZiTy.ExceptionHandling;
using ZiTy.Repositories.Implementations;
using ZiTy.Repositories.Interfaces;
using ZiTy.Services.Implementations;
using ZiTy.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

// Retrieve environment variables
var MySQLServer = Environment.GetEnvironmentVariable("MYSQL_SERVER") ?? throw new ArgumentException("MYSQL_SERVER is missing.");
var MySQLPort = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? throw new ArgumentException("MYSQL_PORT is missing.");
var MySQLDatabase = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? throw new ArgumentException("MYSQL_DATABASE is missing.");
var MySQLUser = Environment.GetEnvironmentVariable("MYSQL_USER") ?? throw new ArgumentException("MYSQL_USER is missing.");
var MySQLPassword = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? throw new ArgumentException("MYSQL_PASSWORD is missing.");

// Convert mysqlPort to integer
if (!int.TryParse(MySQLPort, out int port))
{
    throw new ArgumentException($"Invalid value '{MySQLPort}' for 'MYSQL_PORT'.");
}

var connectionString = $"Server={MySQLServer};Port={port};Database={MySQLDatabase};User={MySQLUser};Password={MySQLPassword}";

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext with MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register repositories and services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Register exception handling
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(); // enables tracking/returning ProblemDetails to a user

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
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();
