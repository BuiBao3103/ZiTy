using zity.DTOs.Surveys;
using zity.Models;
using zity.Utilities;

namespace zity.Repositories.Interfaces
{
    public interface ISurveyRepository
    {
        Task<PaginatedResult<Survey>> GetAllAsync(SurveyQueryDTO query);
        Task<Survey?> GetByIdAsync(int id, string? includes = null);
        Task<Survey> CreateAsync(Survey survey);
        Task<Survey> UpdateAsync(Survey survey);
        Task DeleteAsync(int id);
    }
}
