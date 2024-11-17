using Apartment.Application.Core.Utilities;
using Apartment.Application.DTOs;
using Apartment.Application.DTOs.Apartments;
using Apartment.Application.Interfaces;
using AutoMapper;
using Apartment.Domain.Core.Repositories;
using Apartment.Domain.Core.Specifications;
using Apartment.Domain.Entities;
using Apartment.Domain.Exceptions;

namespace Apartment.Application.Services;

public class ApartmentService(IUnitOfWork unitOfWork, IMapper mapper) : IApartmentService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ApartmentDTO> CreateAsync(ApartmentCreateDTO createDTO)
    {
        var apartment = await _unitOfWork.Repository<Apartment.Domain.Entities.Apartment>().AddAsync(_mapper.Map<Apartment.Domain.Entities.Apartment>(createDTO));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ApartmentDTO>(apartment);
    }

    public async Task DeleteAsync(string id)
    {
        var existingApartment = await _unitOfWork.Repository<Apartment.Domain.Entities.Apartment>().GetByIdAsync(id)
               ?? throw new EntityNotFoundException(nameof(Apartment), id);
        _unitOfWork.Repository<Apartment.Domain.Entities.Apartment>().Delete(existingApartment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PaginatedResult<ApartmentDTO>> GetAllAsync(ApartmentQueryDTO query)
    {
        var filterExpression = query.BuildFilterCriteria<Apartment.Domain.Entities.Apartment>(a => a.DeletedAt == null);
        var spec = new BaseSpecification<Apartment.Domain.Entities.Apartment>(filterExpression);
        var totalCount = await _unitOfWork.Repository<Apartment.Domain.Entities.Apartment>().CountAsync(spec);
        query.Includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<Apartment.Domain.Entities.Apartment>().ListAsync(spec);
        return new PaginatedResult<ApartmentDTO>(
            data.Select(_mapper.Map<ApartmentDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }

    public async Task<ApartmentDTO> GetByIdAsync(string id, string? includes = null)
    {
        var spec = new BaseSpecification<Apartment.Domain.Entities.Apartment>(a => a.DeletedAt == null && a.Id == id);
        includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        var apartment = await _unitOfWork.Repository<Apartment.Domain.Entities.Apartment>().FirstOrDefaultAsync(spec)
            ?? throw new EntityNotFoundException(nameof(Apartment), id);
        return _mapper.Map<ApartmentDTO>(apartment);
    }

    public async Task<ApartmentDTO> PatchAsync(string id, ApartmentPatchDTO patchDTO)
    {
        var existingApartment = await _unitOfWork.Repository<Apartment.Domain.Entities.Apartment>().GetByIdAsync(id)
                 ?? throw new EntityNotFoundException(nameof(Apartment), id);
        _mapper.Map(patchDTO, existingApartment);
        _unitOfWork.Repository<Apartment.Domain.Entities.Apartment>().Update(existingApartment);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ApartmentDTO>(existingApartment);
    }

    public async Task<ApartmentDTO> UpdateAsync(string id, ApartmentUpdateDTO updateDTO)
    {
        var existingApartment = await _unitOfWork.Repository<Apartment.Domain.Entities.Apartment>().GetByIdAsync(id)
               ?? throw new EntityNotFoundException(nameof(Apartment), id);
        _mapper.Map(updateDTO, existingApartment);
        _unitOfWork.Repository<Apartment.Domain.Entities.Apartment>().Update(existingApartment);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ApartmentDTO>(existingApartment);
    }
}
