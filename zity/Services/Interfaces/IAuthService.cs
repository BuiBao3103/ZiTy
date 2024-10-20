namespace zity.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(string username);
    }
}
