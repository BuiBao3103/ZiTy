﻿using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Bills;

public class BillQueryDTO : BaseQueryDTO
{
    [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? Id { get; set; }

    [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w-,]*$", ErrorMessage = "Invalid filter format")]
    public string? Monthly { get; set; }

    [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? RelationshipId { get; set; }

    [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? Relationship_UserId { get; set; }
    [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? Relationship_ApartmentId { get; set; }
    public string? Status { get; set; }

}


