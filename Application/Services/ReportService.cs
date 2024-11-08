using Application.DTOs;
using Application.DTOs.Reports;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Repositories;
using Domain.Core.Specifications;
using Domain.Entities;

namespace Application.Services;

public class ReportService(IUnitOfWork unitOfWork, IMapper mapper) : IReportService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task<PaginatedResult<ReportDTO>> GetAllAsync(ReportQueryDTO query)
    {
        var spec = new BaseSpecification<Report>(a => a.DeletedAt == null);
        var data = await _unitOfWork.Repository<Report>().ListAsync(spec);
        var totalCount = await _unitOfWork.Repository<Report>().CountAsync(spec);
        return new PaginatedResult<ReportDTO>(
            data.Select(_mapper.Map<ReportDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }

    public async Task<ReportDTO> GetByIdAsync(int id, string? includes = null)
    {
        var report = await _unitOfWork.Repository<Report>().GetByIdAsync(id)
           //?? throw new EntityNotFoundException(nameof(Report), id);
           ?? throw new Exception(nameof(Report));
        return _mapper.Map<ReportDTO>(report);
    }

    public async Task<ReportDTO> CreateAsync(ReportCreateDTO createDTO)
    {
        var report = await _unitOfWork.Repository<Report>().AddAsync(_mapper.Map<Report>(createDTO));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ReportDTO>(report);
    }

    public async Task<ReportDTO> UpdateAsync(int id, ReportUpdateDTO updateDTO)
    {
        var existingReport = await _unitOfWork.Repository<Report>().GetByIdAsync(id)
           //?? throw new EntityNotFoundException(nameof(Report), id);
           ?? throw new Exception(nameof(Report));
        _mapper.Map(updateDTO, existingReport);
        _unitOfWork.Repository<Report>().Update(existingReport);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ReportDTO>(existingReport);
    }

    public async Task<ReportDTO> PatchAsync(int id, ReportPatchDTO patchDTO)
    {
        var existingReport = await _unitOfWork.Repository<Report>().GetByIdAsync(id)
            //?? throw new EntityNotFoundException(nameof(Report), id);
            ?? throw new Exception(nameof(Report));
        _mapper.Map(patchDTO, existingReport);
        _unitOfWork.Repository<Report>().Update(existingReport);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ReportDTO>(existingReport);
    }

    public async Task DeleteAsync(int id)
    {
        var existingReport = await _unitOfWork.Repository<Report>().GetByIdAsync(id)
            //?? throw new EntityNotFoundException(nameof(Report), id);
            ?? throw new Exception(nameof(Report));
        _unitOfWork.Repository<Report>().Delete(existingReport);
        await _unitOfWork.SaveChangesAsync();
    }
}
