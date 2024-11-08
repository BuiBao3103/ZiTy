﻿using Domain.Core.Models;

namespace Domain.Entities;
public partial class Setting : BaseEntity
{
    public int Id { get; set; }

    public string CurrentMonthly { get; set; } = null!;

    public string SystemStatus { get; set; } = null!;

    public float RoomPricePerM2 { get; set; }

    public float WaterPricePerM3 { get; set; }

    public float RoomVat { get; set; }

    public int WaterVat { get; set; }

    public int EnvProtectionTax { get; set; }
}
