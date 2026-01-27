using SmartBizAI.Shared.Enums;

namespace SmartBizAI.Shared.DTOs;

public sealed class InvoiceDto
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public InvoiceStatus Status { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
}
