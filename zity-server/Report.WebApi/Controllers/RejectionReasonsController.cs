using Report.Application.DTOs.RejectionReasons;
using Report.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Report.WebApi.Controllers;

[Route("api/rejectionReasons")]
[ApiController]

public class RejectionReasonsController(IRejectionReasonService rejectionReasonService) : ControllerBase
{
   
}
