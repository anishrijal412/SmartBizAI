using System.ComponentModel.DataAnnotations;
using SmartBizAI.Shared.Enums;

namespace SmartBizAI.Api.Entities;

public sealed class Invoice
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string CustomerName { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Amount { get; set; }

    public InvoiceStatus Status { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime DueDate { get; set; }
}
