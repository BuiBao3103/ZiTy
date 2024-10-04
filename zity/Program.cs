using CloudinaryDotNet;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.ExceptionHandling;
using zity.Repositories.Implementations;
using zity.Repositories.Interfaces;
using zity.Services.Implementations;
using zity.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

// Retrieve environment variables for MySQL
var MySQLServer = Environment.GetEnvironmentVariable("MYSQL_SERVER") ?? throw new ArgumentException("MYSQL_SERVER is missing.");
var MySQLPort = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? throw new ArgumentException("MYSQL_PORT is missing.");
var MySQLDatabase = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? throw new ArgumentException("MYSQL_DATABASE is missing.");
var MySQLUser = Environment.GetEnvironmentVariable("MYSQL_USER") ?? throw new ArgumentException("MYSQL_USER is missing.");
var MySQLPassword = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? throw new ArgumentException("MYSQL_PASSWORD is missing.");

// Convert MySQL port to integer
if (!int.TryParse(MySQLPort, out int port))
{
    throw new ArgumentException($"Invalid value '{MySQLPort}' for 'MYSQL_PORT'.");
}

var connectionString = $"Server={MySQLServer};Port={port};Database={MySQLDatabase};User={MySQLUser};Password={MySQLPassword}";

// Retrieve environment variables for Cloudinary
var cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME") ?? throw new ArgumentException("CLOUDINARY_CLOUD_NAME is missing.");
var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY") ?? throw new ArgumentException("CLOUDINARY_API_KEY is missing.");
var apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET") ?? throw new ArgumentException("CLOUDINARY_API_SECRET is missing.");

// Configure Cloudinary account
Account cloudinaryAccount = new(cloudName, apiKey, apiSecret);
Cloudinary cloudinary = new(cloudinaryAccount);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext with MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRelationshipRepository, RelationshipRepository>();

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRelationshipService, RelationshipService>();
builder.Services.AddScoped<IMediaService, MediaService>();

// Register Cloudinary as a singleton service
builder.Services.AddSingleton(cloudinary);

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
