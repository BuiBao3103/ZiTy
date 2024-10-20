using System.Threading.Tasks;
using zity.DTOs.Questions;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;
namespace zity.Services.Implementations
{
    public class QuestionService(IQuestionRepository questionRepository) : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository = questionRepository;

        public async Task<PaginatedResult<QuestionDTO>> GetAllAsync(QuestionQueryDTO queryParam)
        {
            var pageQuestions = await _questionRepository.GetAllAsync(queryParam);

            return new PaginatedResult<QuestionDTO>(
                pageQuestions.Contents.Select(QuestionMapper.ToDTO).ToList(),
                pageQuestions.TotalItems,
                pageQuestions.Page,
                pageQuestions.PageSize);
        }


        public async Task<QuestionDTO?> GetByIdAsync(int id, string? includes)
        {
            var question = await _questionRepository.GetByIdAsync(id, includes);
            return question != null ? QuestionMapper.ToDTO(question) : null;
        }
        public async Task<QuestionDTO> CreateAsync(QuestionCreateDTO createDTO)
        {
            var question = QuestionMapper.ToModelFromCreate(createDTO);
            return QuestionMapper.ToDTO(await _questionRepository.CreateAsync(question));
        }
        public async Task<QuestionDTO?> UpdateAsync(int id, QuestionUpdateDTO updateDTO)
        {
            var existingQuestion = await _questionRepository.GetByIdAsync(id, null);
            if (existingQuestion == null)
            {
                return null;
            }

            QuestionMapper.UpdateModelFromUpdate(existingQuestion, updateDTO);
            var updatedQuestion = await _questionRepository.UpdateAsync(existingQuestion);
            return QuestionMapper.ToDTO(updatedQuestion);
        }

        public async Task<QuestionDTO?> PatchAsync(int id, QuestionPatchDTO patchDTO)
        {
            var existingQuestion = await _questionRepository.GetByIdAsync(id, null);
            if (existingQuestion == null)
            {
                return null;
            }

            QuestionMapper.PatchModelFromPatch(existingQuestion, patchDTO);
            var patchedQuestion = await _questionRepository.UpdateAsync(existingQuestion);
            return QuestionMapper.ToDTO(patchedQuestion);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _questionRepository.DeleteAsync(id);
        }

    }
}