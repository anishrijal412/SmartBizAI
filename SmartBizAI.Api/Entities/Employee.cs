using System.ComponentModel.DataAnnotations;

namespace SmartBizAI.Api.Entities;

public sealed class Employee
{
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(30)]
    public string? Phone { get; set; }

    [Required]
    [MaxLength(100)]
    public string Department { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string JobTitle { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Salary { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
