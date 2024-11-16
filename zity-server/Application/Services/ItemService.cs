using Application.Core.Constants;
using Application.Core.Services;
using Application.Core.Utilities;
using Application.DTOs;
using Application.DTOs.Items;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Repositories;
using Domain.Core.Specifications;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class ItemService(IUnitOfWork unitOfWork, IMapper mapper, IMediaService mediaService) : IItemService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMediaService _mediaService = mediaService;

    public async Task<PaginatedResult<ItemDTO>> GetAllAsync(ItemQueryDTO query)
    {
        var filterExpression = query.BuildFilterCriteria<Item>(a => a.DeletedAt == null);
        var spec = new BaseSpecification<Item>(filterExpression);
        var totalCount = await _unitOfWork.Repository<Item>().CountAsync(spec);
        query.Includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<Item>().ListAsync(spec);
        return new PaginatedResult<ItemDTO>(
            data.Select(_mapper.Map<ItemDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);
    }

    public async Task<ItemDTO> GetByIdAsync(int id, string? includes = null)
    {
        var spec = new BaseSpecification<Item>(a => a.DeletedAt == null && a.Id == id);
        includes?.Split(',').Select(i => char.ToUpper(i[0]) + i[1..]).ToList().ForEach(spec.AddInclude);
        var item = await _unitOfWork.Repository<Item>().FirstOrDefaultAsync(spec)
            ?? throw new EntityNotFoundException(nameof(Item), id);
        return _mapper.Map<ItemDTO>(item);
    }

    public async Task<ItemDTO> CreateAsync(ItemCreateDTO createDTO)
    {
        var item = await _unitOfWork.Repository<Item>().AddAsync(_mapper.Map<Item>(createDTO));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ItemDTO>(item);
    }

    public async Task<ItemDTO> UpdateAsync(int id, ItemUpdateDTO updateDTO)
    {
        var existingItem = await _unitOfWork.Repository<Item>().GetByIdAsync(id)
           ?? throw new EntityNotFoundException(nameof(Item), id);
        _mapper.Map(updateDTO, existingItem);
        _unitOfWork.Repository<Item>().Update(existingItem);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ItemDTO>(existingItem);
    }

    public async Task<ItemDTO> PatchAsync(int id, ItemPatchDTO patchDTO)
    {
        var existingItem = await _unitOfWork.Repository<Item>().GetByIdAsync(id)
           ?? throw new EntityNotFoundException(nameof(Item), id);
        _mapper.Map(patchDTO, existingItem);
        _unitOfWork.Repository<Item>().Update(existingItem);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ItemDTO>(existingItem);
    }

    public async Task DeleteAsync(int id)
    {
        var existingItem = await _unitOfWork.Repository<Item>().GetByIdAsync(id)
            ?? throw new EntityNotFoundException(nameof(Item), id);
        _unitOfWork.Repository<Item>().Delete(existingItem);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<ItemDTO> UploadImageAsync(int id, IFormFile file)
    {
        {
            var item = await _unitOfWork.Repository<Item>().GetByIdAsync(id)
                    ?? throw new EntityNotFoundException(nameof(Item), id);
            if (!string.IsNullOrEmpty(item.Image))
            {
                await _mediaService.DeleteImageAsync(item.Image, CloudinaryConstants.ITEM_IMAGES_FOLDER);
            }
            var imageUrl = await _mediaService.UploadImageAsync(file, CloudinaryConstants.ITEM_IMAGES_FOLDER);
            item.Image = imageUrl;
            _unitOfWork.Repository<Item>().Update(item);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ItemDTO>(item);
        }
    }
}
