﻿using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Relationships;

public class RelationshipCreateDTO
{
    [Required]
    public string Role { get; set; } = null!;
    [Required]
    public int? UserId { get; set; }
    [Required]
    public string ApartmentId { get; set; } = null!;
}

