using zity.Data;
using zity.DTOs.Users;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Utilities;

public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<PaginatedResult<User>> GetAllAsync(UserQueryDTO queryParam)
    {
        var filterParams = new Dictionary<string, string?>
        {
            { "Id", queryParam.Id },
            { "Username", queryParam.Username }
        };

        var userQuery = _dbContext.Users
            .Where(u => u.DeletedAt == null)
            .ApplyIncludes(queryParam.Includes)
            .ApplyFilters(filterParams)
            .ApplySorting(queryParam.Sort)
            .ApplyPaginationAsync(queryParam.Page, queryParam.PageSize);

        return await userQuery;
    }
}
