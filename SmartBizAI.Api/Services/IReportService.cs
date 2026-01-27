using SmartBizAI.Shared.DTOs.Reports;

namespace SmartBizAI.Api.Services;

public interface IReportService
{
    Task<SummaryReportDto> GetSummaryAsync(int month, int year, CancellationToken ct);
    Task<List<MonthlyReportItemDto>> GetMonthlyAsync(int year, CancellationToken ct);
    Task<string> GetDatabaseSummaryAsync(CancellationToken ct);
}
