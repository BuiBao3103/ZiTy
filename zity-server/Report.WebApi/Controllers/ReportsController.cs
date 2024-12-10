using Report.Application.DTOs.Reports;
using Report.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Report.WebApi.Controllers;

[Route("api/reports")]
[ApiController]
public class ReportsController(IReportService reportService) : ControllerBase
{
   
}
