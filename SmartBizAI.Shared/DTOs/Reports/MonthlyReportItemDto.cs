namespace SmartBizAI.Shared.DTOs.Reports;

public sealed class MonthlyReportItemDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal NetProfit { get; set; }
}
