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

public class ReportService(IUnitOfWork unitOfWork, IMapper mapper, HttpClient httpClient) 
{ 

}
