using Application.DTOs.Settings;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/settings")]
[ApiController]
public class SettingsController(ISettingService settingService) : ControllerBase
{
    private readonly ISettingService _settingService = settingService;
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _settingService.GetSetting());
    }

    [HttpPatch]
    public async Task<IActionResult> Patch([FromBody] SettingPatchDTO settingPatchDTO)
    {
        return Ok(await _settingService.PatchSetting(settingPatchDTO));
    }

    [HttpPost("transition/prepayment")]
    public async Task<IActionResult> TransitionToPrepayment()
    {
        return Ok(await _settingService.TransitionToPrepayment());
    }

    [HttpPost("transition/payment")]
    public async Task<IActionResult> TransitionToPayment()
    {
        return Ok(await _settingService.TransitionToPayment());
    }

    [HttpPost("transition/overdue")]
    public async Task<IActionResult> TransitionToOverdue()
    {
        return Ok(await _settingService.TransitionToOverdue());
    }

    [HttpPost("transition/delinquent")]
    public async Task<IActionResult> TransitionToDelinquent()
    {
        return Ok(await _settingService.TransitionToDelinquent());
    }
}
