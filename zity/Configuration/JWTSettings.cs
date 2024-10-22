namespace zity.Configuration
{
    public class JWTSettings
    {
        public string AccessTokenKey { get; set; } = null!;
        public string RefreshTokenKey { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int AccessExpirationInMinutes { get; set; }
        public int RefreshExpirationInDays { get; set; }
    }
}
