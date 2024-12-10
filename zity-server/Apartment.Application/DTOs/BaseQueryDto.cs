using System.ComponentModel.DataAnnotations;
using Apartment.Application.Core.Constants;

namespace Apartment.Application.DTOs;

public class BaseQueryDTO
{
    [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0")]
    public int Page { get; set; } = PaginationConstants.DEFAULT_PAGE;

    [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100")]
    public int PageSize { get; set; } = PaginationConstants.DEFAULT_PAGE_SIZE;

    [RegularExpression(@"^[-+]?[a-zA-Z]+(,[-+]?[a-zA-Z]+)*$", ErrorMessage = "Invalid sort format")]
    public string? Sort { get; set; }
    public string? Includes { get; set; }
}
