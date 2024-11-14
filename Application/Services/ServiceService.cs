using Application.Core.Utilities;
using Application.DTOs;
using Application.DTOs.Services;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Repositories;
using Domain.Core.Specifications;
using Domain.Entities;
using Domain.Exceptions;


namespace Application.Services;

public class ServiceService(IUnitOfWork unitOfWork, IMapper mapper) : IServiceService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<ServiceDTO>> GetAllAsync(ServiceQueryDTO query)
    {
        var filterExpression = query.BuildFilterCriteria<Service>(a => a.DeletedAt == null);
        var spec = new BaseSpecification<Service>(filterExpression);
        var totalCount = await _unitOfWork.Repository<Service>().CountAsync(spec);
        query.Includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<Service>().ListAsync(spec);
        return new PaginatedResult<ServiceDTO>(
            data.Select(_mapper.Map<ServiceDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }

    public async Task<ServiceDTO> GetByIdAsync(int id, string? includes = null)
    {
        var spec = new BaseSpecification<Service>(a => a.DeletedAt == null && a.Id == id);
        includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        var service = await _unitOfWork.Repository<Service>().FirstOrDefaultAsync(spec)
            ?? throw new EntityNotFoundException(nameof(Service), id);
        return _mapper.Map<ServiceDTO>(service);
    }

    public async Task<ServiceDTO> CreateAsync(ServiceCreateDTO createDTO)
    {
        var service = await _unitOfWork.Repository<Service>().AddAsync(_mapper.Map<Service>(createDTO));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ServiceDTO>(service);
    }

    public async Task<ServiceDTO> UpdateAsync(int id, ServiceUpdateDTO updateDTO)
    {
        var existingService = await _unitOfWork.Repository<Service>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Service), id);
        _mapper.Map(updateDTO, existingService);
        _unitOfWork.Repository<Service>().Update(existingService);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ServiceDTO>(existingService);
    }

    public async Task<ServiceDTO> PatchAsync(int id, ServicePatchDTO patchDTO)
    {
        var existingService = await _unitOfWork.Repository<Service>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Service), id);
        _mapper.Map(patchDTO, existingService);
        _unitOfWork.Repository<Service>().Update(existingService);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ServiceDTO>(existingService);
    }

    public async Task DeleteAsync(int id)
    {
        var existingService = await _unitOfWork.Repository<Service>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Service), id);
        _unitOfWork.Repository<Service>().Delete(existingService);
        await _unitOfWork.SaveChangesAsync();
    }
}

