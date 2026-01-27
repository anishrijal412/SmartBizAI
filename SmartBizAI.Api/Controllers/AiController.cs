using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBizAI.Api.Services;
using SmartBizAI.Shared.DTOs.Ai;

namespace SmartBizAI.Api.Controllers;

[ApiController]
[Route("api/ai")]
public sealed class AiController : ControllerBase
{
    private readonly IAiService _aiService;
    private readonly IReportService _reportService;

    public AiController(IAiService aiService, IReportService reportService)
    {
        _aiService = aiService;
        _reportService = reportService;
    }

    [HttpPost("chat")]
    [Authorize]
    public async Task<ActionResult<AiChatResponseDto>> Chat([FromBody] AiChatRequestDto request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest("Message is required.");
        }

        var role = User.FindFirstValue(ClaimTypes.Role) ?? "User";
        var summary = await _reportService.GetDatabaseSummaryAsync(ct);
        var response = await _aiService.ChatAsync(request.Message, role, summary, ct);

        if (response.Response.StartsWith("OpenAI API key is missing", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}
