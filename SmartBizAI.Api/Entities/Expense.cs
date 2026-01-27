using System.ComponentModel.DataAnnotations;

namespace SmartBizAI.Api.Entities;

public sealed class Expense
{
    public int Id { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    [MaxLength(150)]
    public string Category { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }
}
