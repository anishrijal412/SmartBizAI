using Microsoft.EntityFrameworkCore;
using SmartBizAI.Api.Data;
using SmartBizAI.Shared.DTOs.Reports;
using SmartBizAI.Shared.Enums;

namespace SmartBizAI.Api.Services;

public sealed class ReportService : IReportService
{
    private readonly AppDbContext _db;

    public ReportService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<SummaryReportDto> GetSummaryAsync(int month, int year, CancellationToken ct)
    {
        var start = new DateTime(year, month, 1);
        var end = start.AddMonths(1);

        var incomeTotal = await _db.Incomes
            .AsNoTracking()
            .Where(i => i.Date >= start && i.Date < end)
            .SumAsync(i => (decimal?)i.Amount, ct) ?? 0m;

        var expenseTotal = await _db.Expenses
            .AsNoTracking()
            .Where(e => e.Date >= start && e.Date < end)
            .SumAsync(e => (decimal?)e.Amount, ct) ?? 0m;

        var invoiceQuery = _db.Invoices.AsNoTracking().Where(i => i.IssueDate >= start && i.IssueDate < end);
        var totalInvoices = await invoiceQuery.CountAsync(ct);
        var paidInvoices = await invoiceQuery.CountAsync(i => i.Status == InvoiceStatus.Paid, ct);
        var overdueInvoices = await invoiceQuery.CountAsync(i => i.Status == InvoiceStatus.Overdue, ct);
        var pendingInvoices = await invoiceQuery.CountAsync(i => i.Status == InvoiceStatus.Pending, ct);

        return new SummaryReportDto
        {
            Month = month,
            Year = year,
            TotalIncome = incomeTotal,
            TotalExpenses = expenseTotal,
            NetProfit = incomeTotal - expenseTotal,
            TotalInvoices = totalInvoices,
            PaidInvoices = paidInvoices,
            OverdueInvoices = overdueInvoices,
            PendingInvoices = pendingInvoices
        };
    }

    public async Task<List<MonthlyReportItemDto>> GetMonthlyAsync(int year, CancellationToken ct)
    {
        var results = new List<MonthlyReportItemDto>();
        for (var month = 1; month <= 12; month++)
        {
            var summary = await GetSummaryAsync(month, year, ct);
            results.Add(new MonthlyReportItemDto
            {
                Month = month,
                Year = year,
                TotalIncome = summary.TotalIncome,
                TotalExpenses = summary.TotalExpenses,
                NetProfit = summary.NetProfit
            });
        }

        return results;
    }

    public async Task<string> GetDatabaseSummaryAsync(CancellationToken ct)
    {
        var employees = await _db.Employees.AsNoTracking().CountAsync(ct);
        var products = await _db.Products.AsNoTracking().CountAsync(ct);
        var incomes = await _db.Incomes.AsNoTracking().CountAsync(ct);
        var expenses = await _db.Expenses.AsNoTracking().CountAsync(ct);
        var invoices = await _db.Invoices.AsNoTracking().CountAsync(ct);

        var totalIncome = await _db.Incomes.AsNoTracking().SumAsync(i => (decimal?)i.Amount, ct) ?? 0m;
        var totalExpenses = await _db.Expenses.AsNoTracking().SumAsync(e => (decimal?)e.Amount, ct) ?? 0m;
        var net = totalIncome - totalExpenses;

        return $"Employees: {employees}, Products: {products}, Incomes: {incomes}, Expenses: {expenses}, Invoices: {invoices}, TotalIncome: {totalIncome:0.00}, TotalExpenses: {totalExpenses:0.00}, Net: {net:0.00}.";
    }
}
