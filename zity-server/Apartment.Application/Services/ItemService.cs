using Apartment.Application.Core.Services;
using Apartment.Application.Core.Utilities;
using Apartment.Application.DTOs;
using Apartment.Application.DTOs.Items;
using Apartment.Application.Interfaces;
using AutoMapper;
using Apartment.Domain.Core.Repositories;
using Apartment.Domain.Core.Specifications;
using Apartment.Domain.Entities;
using Apartment.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Apartment.Application.Core.Constants;
using System.Net.Http;
using Newtonsoft.Json;
using Apartment.Application.DTOs.IdentityService;

namespace Apartment.Application.Services;

public class ItemService(IUnitOfWork unitOfWork, IMapper mapper, IMediaService mediaService, HttpClient httpClient) : IItemService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMediaService _mediaService = mediaService;

    private readonly HttpClient _httpClient = httpClient;


    public async Task<PaginatedResult<ItemDTO>> GetAllAsync(ItemQueryDTO query)
    {
        var filterExpression = query.BuildFilterCriteria<Item>(a => a.DeletedAt == null);
        var spec = new BaseSpecification<Item>(filterExpression);
        var totalCount = await _unitOfWork.Repository<Item>().CountAsync(spec);
        var includes = query.Includes?.Split(',').Select(include =>
            char.ToUpper(include[0]) + include.Substring(1)).ToList() ?? new List<string>();

        foreach (string include in includes)
        {
            if (include.StartsWith("User")) continue;
            spec.AddInclude(include);
        }
        if (!string.IsNullOrEmpty(query.Sort))
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);
        var data = await _unitOfWork.Repository<Item>().ListAsync(spec);

        var paginatedData = new PaginatedResult<ItemDTO>(
          data.Select(_mapper.Map<ItemDTO>).ToList(),
          totalCount,
          query.Page,
          query.PageSize);


        // Nếu cần thông tin Relationship, tải song song thông qua HTTP client
        if (includes.Contains("User"))
        {
            var relationshipTasks = paginatedData.Contents.Select(async (item, index) =>
            {
                var relationshipsResponse = await _httpClient.GetStringAsync($"http://localhost:8080/api/users/{item.UserId}");
                var user = JsonConvert.DeserializeObject<UserDTO>(relationshipsResponse);
                paginatedData.Contents[index].User = user;
            });

            await Task.WhenAll(relationshipTasks); // Đợi tất cả các tác vụ HTTP hoàn thành
        }
        return paginatedData;
    }

    public async Task<ItemDTO> GetByIdAsync(int id, string? includes = null)
    {
        var spec = new BaseSpecification<Item>(a => a.DeletedAt == null && a.Id == id);
        var includesList = includes?.Split(',').Select(include =>
            char.ToUpper(include[0]) + include.Substring(1)).ToList() ?? new List<string>();

        foreach (string include in includesList)
        {
            if (include.StartsWith("User")) continue;
            spec.AddInclude(include);
        }
        var item = await _unitOfWork.Repository<Item>().FirstOrDefaultAsync(spec)
            ?? throw new EntityNotFoundException(nameof(Item), id);

        var itemDTO = _mapper.Map<ItemDTO>(item);
        if (includesList.Contains("User"))
        {
            var relationshipsResponse = await _httpClient.GetStringAsync($"http://localhost:8080/api/users/{item.UserId}");
            var user = JsonConvert.DeserializeObject<UserDTO>(relationshipsResponse);
            itemDTO.User = user;
        }
        return itemDTO;
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
