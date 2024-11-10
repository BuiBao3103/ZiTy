using Application.Core.Constants;
using Application.DTOs.Settings;
using Application.Interfaces;
using AutoMapper;
using Domain.Core.Repositories;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;

namespace Application.Services;
public class SettingService(IUnitOfWork unitOfWork, IMapper mapper) : ISettingService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<SettingDTO> GetSetting()
    {
        var setting = await _unitOfWork.Repository<Setting>().GetByIdAsync(SettingConstants.SettingId)
          ?? throw new EntityNotFoundException(nameof(Answer), SettingConstants.SettingId);
        return _mapper.Map<SettingDTO>(setting);
    }
    public async Task<SettingDTO> PatchSetting(SettingPatchDTO patchDTO)
    {

        var existingSetting = await _unitOfWork.Repository<Setting>().GetByIdAsync(SettingConstants.SettingId)
            ?? throw new EntityNotFoundException(nameof(Service), SettingConstants.SettingId);
        _mapper.Map(patchDTO, existingSetting);
        _unitOfWork.Repository<Setting>().Update(existingSetting);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SettingDTO>(existingSetting);
    }

    public async Task<SettingDTO> TransitionToDelinquent()
    {
        var existingSetting = await _unitOfWork.Repository<Setting>().GetByIdAsync(SettingConstants.SettingId)
            ?? throw new EntityNotFoundException(nameof(Service), SettingConstants.SettingId);

        existingSetting.SystemStatus = SystemStatusEnum.DELINQUENT;

        _unitOfWork.Repository<Setting>().Update(existingSetting);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SettingDTO>(existingSetting);
    }

    public async Task<SettingDTO> TransitionToOverdue()
    {
        var existingSetting = await _unitOfWork.Repository<Setting>().GetByIdAsync(SettingConstants.SettingId)
             ?? throw new EntityNotFoundException(nameof(Service), SettingConstants.SettingId);

        existingSetting.SystemStatus = SystemStatusEnum.OVERDUE;

        _unitOfWork.Repository<Setting>().Update(existingSetting);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SettingDTO>(existingSetting);
    }

    public async Task<SettingDTO> TransitionToPayment()
    {
        var existingSetting = await _unitOfWork.Repository<Setting>().GetByIdAsync(SettingConstants.SettingId)
            ?? throw new EntityNotFoundException(nameof(Service), SettingConstants.SettingId);

        existingSetting.SystemStatus = SystemStatusEnum.PAYMENT;

        _unitOfWork.Repository<Setting>().Update(existingSetting);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SettingDTO>(existingSetting);
    }

    public async Task<SettingDTO> TransitionToPrepayment()
    {
        var existingSetting = await _unitOfWork.Repository<Setting>().GetByIdAsync(SettingConstants.SettingId)
           ?? throw new EntityNotFoundException(nameof(Service), SettingConstants.SettingId);
        _unitOfWork.Repository<Setting>().Update(existingSetting);

        existingSetting.SystemStatus = SystemStatusEnum.PREPAYMENT;

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<SettingDTO>(existingSetting);
    }
}
