namespace Application.DTOs.Settings;

public class SettingPatchDTO
{
    public string? CurrentMonthly { get; set; } 
    public string? SystemStatus { get; set; } 
    public float? RoomPricePerM2 { get; set; }
    public float? WaterPricePerM3 { get; set; }
    public float? RoomVat { get; set; }
    public int? WaterVat { get; set; }
    public int? EnvProtectionTax { get; set; }
}
