﻿using System.ComponentModel.DataAnnotations;

namespace Apartment.Application.DTOs.Apartments;

public class ApartmentUpdateDTO
{
    [Required]
    public float? Area { get; set; }
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public int? FloorNumber { get; set; }
    [Required]
    public int? ApartmentNumber { get; set; }
    [Required]
    public string Status { get; set; } = null!;
}

