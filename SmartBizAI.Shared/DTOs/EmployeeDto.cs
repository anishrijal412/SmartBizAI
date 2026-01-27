namespace SmartBizAI.Shared.DTOs;

public sealed class EmployeeDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string Department { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public DateTime CreatedAt { get; set; }
}
