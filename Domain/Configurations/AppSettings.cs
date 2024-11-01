
namespace MyApp.Domain.Configurations;

public class AppSettings
{
    public MySqlSettings MySqlSettings { get; set; } = null!;
    public CloudinarySettings CloudinarySettings { get; set; } = null!;
    public EsmsSettings EsmsSettings { get; set; } = null!;
    public JWTSettings JWTSettings { get; set; } = null!;
    public MailSettings MailSettings { get; set; } = null!;
    public MomoSettings MomoSettings { get; set; } = null!;
    public VNPaySettings VNPaySettings { get; set; } = null!;
    public EndpointSettings EndpointSettings { get; set; } = null!;
}
public class MySqlSettings
{
    public string Server { get; set; } = null!;
    public int Port { get; set; }
    public string Database { get; set; } = null!;
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string GetConnectionString()
    {
        return $"Server={Server};Port={Port};Database={Database};User={User};Password={Password};";
    }
}

public class CloudinarySettings
{
    public string CloudName { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
    public string ApiSecret { get; set; } = null!;
}

public class EsmsSettings
{
    public string ApiKey { get; set; } = null!;
    public string ApiSecret { get; set; } = null!;
    public string BrandName { get; set; } = null!;
}

public class JWTSettings
{
    public string AccessTokenKey { get; set; } = null!;
    public string RefreshTokenKey { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int AccessExpirationInMinutes { get; set; }
    public int RefreshExpirationInDays { get; set; }
}

public class MailSettings
{
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FromEmail { get; set; } = null!;
}

public class MomoSettings
{
    public string MomoApiUrl { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public string AccessKey { get; set; } = null!;
    public string ReturnUrl { get; set; } = null!;
    public string NotifyUrl { get; set; } = null!;
    public string PartnerCode { get; set; } = null!;
}

public class VNPaySettings
{
    public string TmnCode { get; set; } = null!;
    public string HashSecret { get; set; } = null!;
    public string Url { get; set; } = null!;
}
public class EndpointSettings
{
    public string LoginUrl { get; set; } = null!;
}