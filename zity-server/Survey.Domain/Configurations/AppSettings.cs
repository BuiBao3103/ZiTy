namespace Survey.Domain.Configurations;

public class AppSettings
{
    public MySqlSettings MySqlSettings { get; set; } = null!;

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

