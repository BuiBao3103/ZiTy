using Billing.Application;
using Billing.Infrastructure;
using Billing.Infrastructure.ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Billing.Domain.Configurations;
using Billing.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureApplication();
builder.Services.ConfigureInfrastructure();

DotNetEnv.Env.Load();
var appSettings = new AppSettings
{
    MySqlSettings = new MySqlSettings
    {
        Server = Environment.GetEnvironmentVariable("MYSQL_SERVER") ?? throw new ArgumentException("MYSQL_SERVER is missing."),
        Port = int.Parse(Environment.GetEnvironmentVariable("MYSQL_PORT") ?? throw new ArgumentException("MYSQL_PORT is missing.")),
        Database = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? throw new ArgumentException("MYSQL_DATABASE is missing."),
        User = Environment.GetEnvironmentVariable("MYSQL_USER") ?? throw new ArgumentException("MYSQL_USER is missing."),
        Password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? throw new ArgumentException("MYSQL_PASSWORD is missing.")
    },
    VNPaySettings = new VNPaySettings
    {
        TmnCode = Environment.GetEnvironmentVariable("VNP_TMN_CODE") ?? throw new ArgumentException("VNP_TMN_CODE is missing."),
        HashSecret = Environment.GetEnvironmentVariable("VNP_HASH_SECRET") ?? throw new ArgumentException("VNP_HASH_SECRET is missing."),
        Url = Environment.GetEnvironmentVariable("VNP_URL") ?? throw new ArgumentException("VNP_URL is missing.")
    },
    MomoSettings = new MomoSettings
    {
        MomoApiUrl = Environment.GetEnvironmentVariable("MOMO_API_URL") ?? throw new ArgumentException("MOMO_API_URL is missing."),
        SecretKey = Environment.GetEnvironmentVariable("MOMO_SECRET_KEY") ?? throw new ArgumentException("MOMO_SECRET_KEY is missing."),
        AccessKey = Environment.GetEnvironmentVariable("MOMO_ACCESS_KEY") ?? throw new ArgumentException("MOMO_ACCESS_KEY is missing."),
        ReturnUrl = Environment.GetEnvironmentVariable("MOMO_RETURN_URL") ?? throw new ArgumentException("MOMO_RETURN_URL is missing."),
        NotifyUrl = Environment.GetEnvironmentVariable("MOMO_NOTIFY_URL") ?? throw new ArgumentException("MOMO_NOTIFY_URL is missing."),
        PartnerCode = Environment.GetEnvironmentVariable("MOMO_PARTNER_CODE") ?? throw new ArgumentException("MOMO_PARTNER_CODE is missing."),
    },
};
builder.Services.AddSingleton(appSettings);

var connectionString = appSettings.MySqlSettings.GetConnectionString();
builder.Services.AddDbContext<BillingDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Your API",
        Version = "v1"
    });

    // Th�m config cho JWT Authorization
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                      "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                      "Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});
builder.Services.AddHttpContextAccessor();

var corsPolicy = "AllowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


builder.Services.AddAuthorization();
var app = builder.Build();
app.UseCors(corsPolicy);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();