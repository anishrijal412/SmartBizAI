using System.ComponentModel.DataAnnotations;

namespace SmartBizAI.Api.Entities;

public sealed class Product
{
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string SKU { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal UnitPrice { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
