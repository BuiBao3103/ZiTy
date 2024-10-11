using CloudinaryDotNet;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using zity.Configuration; // Import the configuration namespaces
using zity.Data;
using zity.ExceptionHandling;
using zity.Repositories.Implementations;
using zity.Repositories.Interfaces;
using zity.Services.Implementations;
using zity.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

// Configure MySQL settings
var mySqlSettings = new MySqlSettings
{
    Server = Environment.GetEnvironmentVariable("MYSQL_SERVER") ?? throw new ArgumentException("MYSQL_SERVER is missing."),
    Port = int.Parse(Environment.GetEnvironmentVariable("MYSQL_PORT") ?? throw new ArgumentException("MYSQL_PORT is missing.")),
    Database = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? throw new ArgumentException("MYSQL_DATABASE is missing."),
    User = Environment.GetEnvironmentVariable("MYSQL_USER") ?? throw new ArgumentException("MYSQL_USER is missing."),
    Password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? throw new ArgumentException("MYSQL_PASSWORD is missing.")
};

// Configure Mail settings
var mailSettings = new MailSettings
{
    Host = Environment.GetEnvironmentVariable("MAILTRAP_HOST") ?? throw new ArgumentException("MAILTRAP_HOST is missing."),
    Port = int.Parse(Environment.GetEnvironmentVariable("MAILTRAP_PORT") ?? throw new ArgumentException("MAILTRAP_PORT is missing.")),
    Username = Environment.GetEnvironmentVariable("MAILTRAP_USERNAME") ?? throw new ArgumentException("MAILTRAP_USERNAME is missing."),
    Password = Environment.GetEnvironmentVariable("MAILTRAP_PASSWORD") ?? throw new ArgumentException("MAILTRAP_PASSWORD is missing."),
    FromEmail = Environment.GetEnvironmentVariable("MAILTRAP_FROM") ?? throw new ArgumentException("MAILTRAP_FROM is missing.")
};

// Configure Cloudinary settings
var cloudinarySettings = new CloudinarySettings
{
    CloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME") ?? throw new ArgumentException("CLOUDINARY_CLOUD_NAME is missing."),
    ApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY") ?? throw new ArgumentException("CLOUDINARY_API_KEY is missing."),
    ApiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET") ?? throw new ArgumentException("CLOUDINARY_API_SECRET is missing.")
};

// Retrieve the login URL from environment variables
var appSettings = new AppSettings
{
    LoginUrl = Environment.GetEnvironmentVariable("LOGIN_URL") ?? throw new ArgumentException("LOGIN_URL is missing.")
};

// Configure Vonage settings
var vonageSettings = new VonageSettings
{
    ApiKey = Environment.GetEnvironmentVariable("VONAGE_API_KEY") ?? throw new ArgumentException("VONAGE_API_KEY is missing."),
    ApiSecret = Environment.GetEnvironmentVariable("VONAGE_API_SECRET") ?? throw new ArgumentException("VONAGE_API_SECRET is missing."),
    BrandName = Environment.GetEnvironmentVariable("VONAGE_BRAND_NAME") ?? throw new ArgumentException("VONAGE_BRAND_NAME is missing.")
};

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext with MySQL
var connectionString = $"Server={mySqlSettings.Server};Port={mySqlSettings.Port};Database={mySqlSettings.Database};User={mySqlSettings.User};Password={mySqlSettings.Password}";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRelationshipRepository, RelationshipRepository>();

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRelationshipService, RelationshipService>();
builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Register Cloudinary as a singleton service
var cloudinaryAccount = new Account(cloudinarySettings.CloudName, cloudinarySettings.ApiKey, cloudinarySettings.ApiSecret);
var cloudinary = new Cloudinary(cloudinaryAccount);
builder.Services.AddSingleton(cloudinary);

// Register Mail settings
builder.Services.AddSingleton(mailSettings);

// Register AppSettings
builder.Services.AddSingleton(appSettings);

// Register Vonage settings
builder.Services.AddSingleton(vonageSettings);

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
