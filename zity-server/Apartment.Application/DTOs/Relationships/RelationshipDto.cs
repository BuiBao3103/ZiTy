﻿using Apartment.Application.DTOs.Apartments;
using Apartment.Application.DTOs.IdentityService;

namespace Apartment.Application.DTOs.Relationships;

public class RelationshipDTO
{
    public int Id { get; set; }

    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int UserId { get; set; }

    public string ApartmentId { get; set; } = null!;

    public ApartmentDTO? Apartment { get; set; } = null;

    public UserDTO? User { get; set; } = null;
}

