using SmartBizAI.Api.Entities;

namespace SmartBizAI.Api.Repositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync(CancellationToken ct);
    Task<Employee?> GetByIdAsync(int id, CancellationToken ct);
    Task<Employee?> GetByEmailAsync(string email, CancellationToken ct);
    Task<Employee> AddAsync(Employee employee, CancellationToken ct);
    Task UpdateAsync(Employee employee, CancellationToken ct);
    Task DeleteAsync(Employee employee, CancellationToken ct);
}
