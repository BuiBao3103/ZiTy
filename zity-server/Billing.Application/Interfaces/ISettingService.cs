using Billing.Application.DTOs.Settings;

namespace Billing.Application.Interfaces;

public interface ISettingService
{
    Task<SettingDTO> GetSetting();
    Task<SettingDTO> PatchSetting(SettingPatchDTO settingPatchDTO);
    Task<SettingDTO> TransitionToPrepayment();
    Task<SettingDTO> TransitionToPayment();
    Task<SettingDTO> TransitionToOverdue();
    Task<SettingDTO> TransitionToDelinquent();
}
