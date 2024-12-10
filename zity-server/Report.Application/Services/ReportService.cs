using AutoMapper;
using Newtonsoft.Json;
using Report.Application.Core.Utilities;
using Report.Application.DTOs;
using Report.Application.DTOs.ApartmentService;
using Report.Application.DTOs.Reports;
using Report.Application.Interfaces;
using Report.Domain.Core.Repositories;
using Report.Domain.Core.Specifications;
using Report.Domain.Exceptions;
using System.Net.Http;

namespace Report.Application.Services;

public class ReportService(IUnitOfWork unitOfWork, IMapper mapper, HttpClient httpClient) : IReportService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HttpClient _httpClient = httpClient;


    public async Task<PaginatedResult<ReportDTO>> GetAllAsync(ReportQueryDTO query)
    {
        var filterExpression = query.BuildFilterCriteria<Report.Domain.Entities.Report>(a => a.DeletedAt == null);
        var spec = new BaseSpecification<Report.Domain.Entities.Report>(filterExpression);
        var totalCount = await _unitOfWork.Repository<Report.Domain.Entities.Report>().CountAsync(spec);

        var includes = query.Includes?.Split(',').Select(include =>
            char.ToUpper(include[0]) + include.Substring(1)).ToList() ?? new List<string>();

        foreach (string include in includes)
        {
            if (include.StartsWith("Relationship")) continue;
            spec.AddInclude(include);
        }

        // Áp dụng sắp xếp
        if (!string.IsNullOrEmpty(query.Sort))
        {
            if (query.Sort.StartsWith("-"))
                spec.ApplyOrderByDescending(query.Sort[1..]);
            else
                spec.ApplyOrderBy(query.Sort);
        }

        // Áp dụng phân trang
        spec.ApplyPaging(query.PageSize * (query.Page - 1), query.PageSize);

        // Lấy dữ liệu từ database
        var data = await _unitOfWork.Repository<Report.Domain.Entities.Report>().ListAsync(spec);

        // Tạo đối tượng PaginatedResult
        var paginatedData = new PaginatedResult<ReportDTO>(
            data.Select(_mapper.Map<ReportDTO>).ToList(),
            totalCount,
            query.Page,
            query.PageSize);

        // Nếu cần thông tin Relationship, tải song song thông qua HTTP client
        if (includes.Contains("Relationship"))
        {
            var relationshipTasks = paginatedData.Contents.Select(async (report, index) =>
            {
                var relationshipsResponse = await _httpClient.GetStringAsync($"http://localhost:8080/api/relationships/{report.RelationshipId}");
                var relationship = JsonConvert.DeserializeObject<RelationshipDTO>(relationshipsResponse);
                paginatedData.Contents[index].Relationship = relationship;
            });

            await Task.WhenAll(relationshipTasks); // Đợi tất cả các tác vụ HTTP hoàn thành
        }

        return paginatedData;
    }


    
}
