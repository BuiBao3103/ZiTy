namespace Billing.Domain.Configurations;

public class AppSettings
{
    public MySqlSettings MySqlSettings { get; set; } = null!;
    public MomoSettings MomoSettings { get; set; } = null!;
    public VNPaySettings VNPaySettings { get; set; } = null!;
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
