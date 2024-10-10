namespace zity.Configuration
{
    public class MySqlSettings
    {
        public string Server { get; set; } = null!;
        public int Port { get; set; }
        public string Database { get; set; } = null!;
        public string User { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
