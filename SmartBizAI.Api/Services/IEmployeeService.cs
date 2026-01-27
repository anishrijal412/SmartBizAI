using SmartBizAI.Shared.DTOs;

namespace SmartBizAI.Api.Services;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> GetAllAsync(CancellationToken ct);
    Task<EmployeeDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<EmployeeDto?> GetByEmailAsync(string email, CancellationToken ct);
    Task<EmployeeDto> CreateAsync(EmployeeDto dto, CancellationToken ct);
    Task<bool> UpdateAsync(int id, EmployeeDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
