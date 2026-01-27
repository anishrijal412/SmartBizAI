namespace SmartBizAI.Shared.DTOs;

public sealed class IncomeDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Source { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string? Notes { get; set; }
}
