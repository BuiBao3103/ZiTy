using Apartment.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Apartment.Application.DTOs.Apartments;
public class ApartmentQueryDTO : BaseQueryDTO
{
    [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? Id { get; set; }
    public string? FloorNumber { get; set; }
    public string? ApartmentNumber { get; set; }
}
