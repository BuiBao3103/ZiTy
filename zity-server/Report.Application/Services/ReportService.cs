using AutoMapper;
using Report.Application.Core.Utilities;
using Report.Application.DTOs;
using Report.Application.DTOs.Reports;
using Report.Application.Interfaces;
using Report.Domain.Core.Repositories;
using Report.Domain.Core.Specifications;
using Report.Domain.Exceptions;

namespace Report.Application.Services;

public class ReportService(IUnitOfWork unitOfWork, IMapper mapper) : IReportService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task<PaginatedResult<ReportDTO>> GetAllAsync(ReportQueryDTO query)
    {
        var filterExpression = query.BuildFilterCriteria<Report.Domain.Entities.Report>(a => a.DeletedAt == null);
        var spec = new BaseSpecification<Report.Domain.Entities.Report>(filterExpression);
        var totalCount = await _unitOfWork.Repository<Report.Domain.Entities.Report>().CountAsync(spec);
        query.Includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<Report.Domain.Entities.Report>().ListAsync(spec);
        return new PaginatedResult<ReportDTO>(
            data.Select(_mapper.Map<ReportDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize); ;
    }

    public async Task<ReportDTO> GetByIdAsync(int id, string? includes = null)
    {
        var spec = new BaseSpecification<Report.Domain.Entities.Report>(a => a.DeletedAt == null && a.Id == id);
        includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        var report = await _unitOfWork.Repository<Report.Domain.Entities.Report>().FirstOrDefaultAsync(spec)
            ?? throw new EntityNotFoundException(nameof(Report), id);
        return _mapper.Map<ReportDTO>(report);
    }

    public async Task<ReportDTO> CreateAsync(ReportCreateDTO createDTO)
    {
        var report = await _unitOfWork.Repository<Report.Domain.Entities.Report>().AddAsync(_mapper.Map<Report.Domain.Entities.Report>(createDTO));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ReportDTO>(report);
    }

    public async Task<ReportDTO> UpdateAsync(int id, ReportUpdateDTO updateDTO)
    {
        var existingReport = await _unitOfWork.Repository<Report.Domain.Entities.Report>().GetByIdAsync(id)
           ?? throw new EntityNotFoundException(nameof(Report), id);
        _mapper.Map(updateDTO, existingReport);
        _unitOfWork.Repository<Report.Domain.Entities.Report>().Update(existingReport);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ReportDTO>(existingReport);
    }

    public async Task<ReportDTO> PatchAsync(int id, ReportPatchDTO patchDTO)
    {
        var existingReport = await _unitOfWork.Repository<Report.Domain.Entities.Report>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Report), id);
        _mapper.Map(patchDTO, existingReport);
        _unitOfWork.Repository<Report.Domain.Entities.Report>().Update(existingReport);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ReportDTO>(existingReport);
    }

    public async Task DeleteAsync(int id)
    {
        var existingReport = await _unitOfWork.Repository<Report.Domain.Entities.Report>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Report), id);
        _unitOfWork.Repository<Report.Domain.Entities.Report>().Delete(existingReport);
        await _unitOfWork.SaveChangesAsync();
    }
}
