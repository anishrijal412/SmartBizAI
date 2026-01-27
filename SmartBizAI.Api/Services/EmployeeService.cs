using SmartBizAI.Api.Entities;
using SmartBizAI.Api.Repositories;
using SmartBizAI.Shared.DTOs;

namespace SmartBizAI.Api.Services;

public sealed class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repo;

    public EmployeeService(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<EmployeeDto>> GetAllAsync(CancellationToken ct)
    {
        var items = await _repo.GetAllAsync(ct);
        return items.Select(MapToDto).ToList();
    }

    public async Task<EmployeeDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        var item = await _repo.GetByIdAsync(id, ct);
        return item == null ? null : MapToDto(item);
    }

    public async Task<EmployeeDto?> GetByEmailAsync(string email, CancellationToken ct)
    {
        var item = await _repo.GetByEmailAsync(email, ct);
        return item == null ? null : MapToDto(item);
    }

    public async Task<EmployeeDto> CreateAsync(EmployeeDto dto, CancellationToken ct)
    {
        var entity = new Employee
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            Department = dto.Department,
            JobTitle = dto.JobTitle,
            Salary = dto.Salary,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repo.AddAsync(entity, ct);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, EmployeeDto dto, CancellationToken ct)
    {
        var existing = await _repo.GetByIdAsync(id, ct);
        if (existing == null)
        {
            return false;
        }

        var entity = new Employee
        {
            Id = id,
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            Department = dto.Department,
            JobTitle = dto.JobTitle,
            Salary = dto.Salary,
            CreatedAt = existing.CreatedAt
        };

        await _repo.UpdateAsync(entity, ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var existing = await _repo.GetByIdAsync(id, ct);
        if (existing == null)
        {
            return false;
        }

        await _repo.DeleteAsync(existing, ct);
        return true;
    }

    private static EmployeeDto MapToDto(Employee entity)
        => new()
        {
            Id = entity.Id,
            FullName = entity.FullName,
            Email = entity.Email,
            Phone = entity.Phone,
            Department = entity.Department,
            JobTitle = entity.JobTitle,
            Salary = entity.Salary,
            CreatedAt = entity.CreatedAt
        };
}
