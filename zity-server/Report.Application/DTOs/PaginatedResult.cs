namespace Report.Application.DTOs;
public class PaginatedResult<T>
{
    public List<T> Contents { get; set; } = [];
    public int TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public PaginatedResult(List<T> contents, int totalItems, int page, int pageSize)
    {
        Contents = contents;
        TotalItems = totalItems;
        Page = page;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
    }
}
