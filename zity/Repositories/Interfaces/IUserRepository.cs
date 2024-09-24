using ZiTy.Models;

namespace ZiTy.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
    }
}
