﻿using System.ComponentModel.DataAnnotations;

namespace Report.Application.DTOs.Reports;

public class ReportQueryDTO : BaseQueryDTO
{
    [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? Id { get; set; }
    [RegularExpression(@"^(eq):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? RelationshipId { get; set; }
}
