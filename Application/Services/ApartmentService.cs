using Application.DTOs;
using Application.DTOs.Apartments;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Repositories;
using Domain.Core.Specifications;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services;

public class ApartmentService(IUnitOfWork unitOfWork, IMapper mapper) : IApartmentService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ApartmentDTO> CreateAsync(ApartmentCreateDTO createDTO)
    {
        var apartment = await _unitOfWork.Repository<Apartment>().AddAsync(_mapper.Map<Apartment>(createDTO));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ApartmentDTO>(apartment);
    }

    public async Task DeleteAsync(string id)
    {
        var existingApartment = await _unitOfWork.Repository<Apartment>().GetByIdAsync(id)
               ?? throw new EntityNotFoundException(nameof(Apartment), id);
        _unitOfWork.Repository<Apartment>().Delete(existingApartment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PaginatedResult<ApartmentDTO>> GetAllAsync(ApartmentQueryDTO query)
    {
        var spec = new BaseSpecification<Apartment>(a => a.DeletedAt == null);
        var totalCount = await _unitOfWork.Repository<Apartment>().CountAsync(spec);
        query.Includes?.Split(',').ToList().ForEach(spec.AddInclude);
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<Apartment>().ListAsync(spec);
        return new PaginatedResult<ApartmentDTO>(
            data.Select(_mapper.Map<ApartmentDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }

    public async Task<ApartmentDTO> GetByIdAsync(string id, string? includes = null)
    {
        var spec = new BaseSpecification<Apartment>(a => a.DeletedAt == null && a.Id == id);
        includes?.Split(',').ToList().ForEach(spec.AddInclude);
        var apartment = await _unitOfWork.Repository<Apartment>().FirstOrDefaultAsync(spec)
            ?? throw new EntityNotFoundException(nameof(Apartment), id);
        return _mapper.Map<ApartmentDTO>(apartment);
    }

    public async Task<ApartmentDTO> PatchAsync(string id, ApartmentPatchDTO patchDTO)
    {
        var existingApartment = await _unitOfWork.Repository<Apartment>().GetByIdAsync(id)
                 ?? throw new EntityNotFoundException(nameof(Apartment), id);
        _mapper.Map(patchDTO, existingApartment);
        _unitOfWork.Repository<Apartment>().Update(existingApartment);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ApartmentDTO>(existingApartment);
    }

    public async Task<ApartmentDTO> UpdateAsync(string id, ApartmentUpdateDTO updateDTO)
    {
        var existingApartment = await _unitOfWork.Repository<Apartment>().GetByIdAsync(id)
               ?? throw new EntityNotFoundException(nameof(Apartment), id);
        _mapper.Map(updateDTO, existingApartment);
        _unitOfWork.Repository<Apartment>().Update(existingApartment);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ApartmentDTO>(existingApartment);
    }
}
