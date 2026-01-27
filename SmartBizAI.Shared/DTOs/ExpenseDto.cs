namespace SmartBizAI.Shared.DTOs;

public sealed class ExpenseDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string? Notes { get; set; }
}
