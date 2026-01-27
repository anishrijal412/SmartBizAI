using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBizAI.Api.Services;
using SmartBizAI.Shared.DTOs.Reports;

namespace SmartBizAI.Api.Controllers;

[ApiController]
[Route("api/reports")]
public sealed class ReportsController : ControllerBase
{
    private readonly IReportService _service;

    public ReportsController(IReportService service)
    {
        _service = service;
    }

    [HttpGet("summary")]
    [Authorize(Roles = "Admin,Accountant,Manager")]
    public async Task<ActionResult<SummaryReportDto>> GetSummary([FromQuery] int month, [FromQuery] int year, CancellationToken ct)
    {
        if (month < 1 || month > 12 || year < 2000)
        {
            return BadRequest("Invalid month/year.");
        }

        var summary = await _service.GetSummaryAsync(month, year, ct);
        return Ok(summary);
    }

    [HttpGet("monthly")]
    [Authorize(Roles = "Admin,Accountant,Manager")]
    public async Task<ActionResult<List<MonthlyReportItemDto>>> GetMonthly([FromQuery] int year, CancellationToken ct)
    {
        if (year < 2000)
        {
            return BadRequest("Invalid year.");
        }

        var report = await _service.GetMonthlyAsync(year, ct);
        return Ok(report);
    }
}
