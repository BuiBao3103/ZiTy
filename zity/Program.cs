using CloudinaryDotNet;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using zity.Configuration;
using zity.Data;
using zity.ExceptionHandling;
using zity.Mappers;
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

// Configure Esms settings
var esmsSettings = new EsmsSettings
{
    ApiKey = Environment.GetEnvironmentVariable("ESMS_API_KEY") ?? throw new ArgumentException("ESMS_API_KEY is missing."),
    ApiSecret = Environment.GetEnvironmentVariable("ESMS_API_SECRET") ?? throw new ArgumentException("ESMS_API_SECRET is missing."),
    BrandName = Environment.GetEnvironmentVariable("ESMS_BRAND_NAME") ?? throw new ArgumentException("ESMS_BRAND_NAME is missing.")
};

// Configure VNPay settings
var vnpaySettings = new VNPaySettings
{
    TmnCode = Environment.GetEnvironmentVariable("VNP_TMN_CODE") ?? throw new ArgumentException("VNP_TMN_CODE is missing."),
    HashSecret = Environment.GetEnvironmentVariable("VNP_HASH_SECRET") ?? throw new ArgumentException("VNP_HASH_SECRET is missing."),
    Url = Environment.GetEnvironmentVariable("VNP_URL") ?? throw new ArgumentException("VNP_URL is missing.")
};

// Configure Momo settings
var momoSettings = new MomoSettings
{
    MomoApiUrl = Environment.GetEnvironmentVariable("MOMO_API_URL") ?? throw new ArgumentException("MOMO_API_URL is missing."),
    SecretKey = Environment.GetEnvironmentVariable("MOMO_SECRET_KEY") ?? throw new ArgumentException("MOMO_SECRET_KEY is missing."),
    AccessKey = Environment.GetEnvironmentVariable("MOMO_ACCESS_KEY") ?? throw new ArgumentException("MOMO_ACCESS_KEY is missing."),
    ReturnUrl = Environment.GetEnvironmentVariable("MOMO_RETURN_URL") ?? throw new ArgumentException("MOMO_RETURN_URL is missing."),
    NotifyUrl = Environment.GetEnvironmentVariable("MOMO_NOTIFY_URL") ?? throw new ArgumentException("MOMO_NOTIFY_URL is missing."),
    PartnerCode = Environment.GetEnvironmentVariable("MOMO_PARTNER_CODE") ?? throw new ArgumentException("MOMO_PARTNER_CODE is missing."),
};

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

// CORS policy
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

// Register DbContext with MySQL
var connectionString = $"Server={mySqlSettings.Server};Port={mySqlSettings.Port};Database={mySqlSettings.Database};User={mySqlSettings.User};Password={mySqlSettings.Password}";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRelationshipRepository, RelationshipRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IOtherAnswerRepository, OtherAnswerRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IRejectionReasonRepository, RejectionReasonRepository>();
builder.Services.AddScoped<IUserAnswerRepository, UserAnswerRepository>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IBillDetailRepository, BillDetailRepository>();
builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ISurveyRepository, SurveyRepository>();

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRelationshipService, RelationshipService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IOtherAnswerService, OtherAnswerService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IRejectionReasonService, RejectionReasonService>();
builder.Services.AddScoped<IUserAnswerService, UserAnswerService>();
builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISmsService, SmsService>();
builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddScoped<IBillDetailService, BillDetailService>();
builder.Services.AddScoped<IApartmentService, ApartmentService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<IVNPayService, VNPayService>();
builder.Services.AddScoped<IMomoService, MomoService>();

// Register Cloudinary as a singleton service
var cloudinaryAccount = new Account(cloudinarySettings.CloudName, cloudinarySettings.ApiKey, cloudinarySettings.ApiSecret);
var cloudinary = new Cloudinary(cloudinaryAccount);
builder.Services.AddSingleton(cloudinary);

// Register Mail settings
builder.Services.AddSingleton(mailSettings);

// Register AppSettings
builder.Services.AddSingleton(appSettings);

// Register ESMS settings
builder.Services.AddSingleton(esmsSettings);

// Register VNPay settings
builder.Services.AddSingleton(vnpaySettings);

// Register Momo settings
builder.Services.AddSingleton(momoSettings);

// Register exception handling
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(); // enables tracking/returning ProblemDetails to a user

// Register AutoMapper
builder.Services.AddAutoMapper(
        typeof(BillMapping),
        typeof(BillDetailMapping),
        typeof(UserMapping),
        typeof(AnswerMapping),
        typeof(OtherAnswerMapping),
        typeof(QuestionMapping),
        typeof(RejectionReasonMapping),
        typeof(UserAnswerMapping),
        typeof(ApartmentMapping),
        typeof(ItemMapping),
        typeof(ReportMapping),
        typeof(ServiceMapping),
        typeof(SurveyMapping)
    );

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

// Use CORS middleware
app.UseCors(corsPolicy);

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
