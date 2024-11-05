using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using Application.Core.Services;
using Domain.Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using MyApp.Domain.Configurations;
using Infrastructure.Services;
using Application.Core.Services;
//using Infrastructure.Services;

namespace Infrastructure;
public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services)
    {
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
        services.AddSingleton(appSettings);

        var connectionString = appSettings.MySqlSettings.GetConnectionString();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IMediaService, MediaService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IVNPayService, VNPayService>();
        services.AddScoped<IMomoService, MomoService>();
        services.AddScoped<ISmsService, SmsService>();
        //services.AddScoped<ILoggerService, LoggerService>();
    }

    public static void MigrateDatabase(this IServiceProvider serviceProvider)
    {
        var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();

        using (var dbContext = new ApplicationDbContext(dbContextOptions))
        {
            dbContext.Database.Migrate();
        }
    }
}
