namespace SmartBizAI.Shared.DTOs.Reports;

public sealed class SummaryReportDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal NetProfit { get; set; }
    public int TotalInvoices { get; set; }
    public int PaidInvoices { get; set; }
    public int OverdueInvoices { get; set; }
    public int PendingInvoices { get; set; }
}
