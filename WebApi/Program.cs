using Application;
using Domain.Configurations;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.ErrorHandling;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
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
    MailSettings = new MailSettings
    {
        Host = Environment.GetEnvironmentVariable("MAILTRAP_HOST") ?? throw new ArgumentException("MAILTRAP_HOST is missing."),
        Port = int.Parse(Environment.GetEnvironmentVariable("MAILTRAP_PORT") ?? throw new ArgumentException("MAILTRAP_PORT is missing.")),
        Username = Environment.GetEnvironmentVariable("MAILTRAP_USERNAME") ?? throw new ArgumentException("MAILTRAP_USERNAME is missing."),
        Password = Environment.GetEnvironmentVariable("MAILTRAP_PASSWORD") ?? throw new ArgumentException("MAILTRAP_PASSWORD is missing."),
        FromEmail = Environment.GetEnvironmentVariable("MAILTRAP_FROM") ?? throw new ArgumentException("MAILTRAP_FROM is missing.")
    },
    CloudinarySettings = new CloudinarySettings
    {
        CloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME") ?? throw new ArgumentException("CLOUDINARY_CLOUD_NAME is missing."),
        ApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY") ?? throw new ArgumentException("CLOUDINARY_API_KEY is missing."),
        ApiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET") ?? throw new ArgumentException("CLOUDINARY_API_SECRET is missing.")
    },
    JWTSettings = new JWTSettings
    {
        AccessTokenKey = Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_KEY") ?? throw new ArgumentException("JWT_ACCESS_TOKEN_KEY is missing."),
        RefreshTokenKey = Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_KEY") ?? throw new ArgumentException("JWT_REFRESH_TOKEN_KEY is missing."),
        Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new ArgumentException("JWT_ISSUER is missing."),
        Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? throw new ArgumentException("JWT_AUDIENCE is missing."),
        AccessExpirationInMinutes = int.Parse(Environment.GetEnvironmentVariable("JWT_ACCESS_EXPIRATION_IN_MINUTES") ?? throw new ArgumentException("JWT_ACCESS_EXPIRATION_IN_MINUTES is missing.")),
        RefreshExpirationInDays = int.Parse(Environment.GetEnvironmentVariable("JWT_REFRESH_EXPIRATION_IN_DAYS") ?? throw new ArgumentException("JWT_REFRESH_EXPIRATION_IN_DAYS is missing.")),
    },
    EsmsSettings = new EsmsSettings
    {
        ApiKey = Environment.GetEnvironmentVariable("ESMS_API_KEY") ?? throw new ArgumentException("ESMS_API_KEY is missing."),
        ApiSecret = Environment.GetEnvironmentVariable("ESMS_API_SECRET") ?? throw new ArgumentException("ESMS_API_SECRET is missing."),
        BrandName = Environment.GetEnvironmentVariable("ESMS_BRAND_NAME") ?? throw new ArgumentException("ESMS_BRAND_NAME is missing.")
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
    EndpointSettings = new EndpointSettings
    {
        LoginUrl = Environment.GetEnvironmentVariable("LOGIN_URL") ?? throw new ArgumentException("LOGIN_URL is missing.")
    }
};
builder.Services.AddSingleton(appSettings);

// Add services to the container.
builder.Services.ConfigureApplication();
builder.Services.ConfigureInfrastructure();

var connectionString = appSettings.MySqlSettings.GetConnectionString();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
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

    // Thêm config cho JWT Authorization
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
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = appSettings.JWTSettings.Issuer,
        ValidAudience = appSettings.JWTSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(appSettings.JWTSettings.AccessTokenKey)),
        ClockSkew = TimeSpan.Zero
    };
}).AddJwtBearer("RefreshToken", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = appSettings.JWTSettings!.Issuer,
        ValidAudience = appSettings.JWTSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(appSettings.JWTSettings.RefreshTokenKey)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(corsPolicy);
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
