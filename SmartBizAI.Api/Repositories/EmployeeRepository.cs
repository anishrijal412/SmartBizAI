using Microsoft.EntityFrameworkCore;
using SmartBizAI.Api.Data;
using SmartBizAI.Api.Entities;

namespace SmartBizAI.Api.Repositories;

public sealed class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _db;

    public EmployeeRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<List<Employee>> GetAllAsync(CancellationToken ct)
        => _db.Employees.AsNoTracking().OrderBy(e => e.FullName).ToListAsync(ct);

    public Task<Employee?> GetByIdAsync(int id, CancellationToken ct)
        => _db.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, ct);

    public Task<Employee?> GetByEmailAsync(string email, CancellationToken ct)
        => _db.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email, ct);

    public async Task<Employee> AddAsync(Employee employee, CancellationToken ct)
    {
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync(ct);
        return employee;
    }

    public async Task UpdateAsync(Employee employee, CancellationToken ct)
    {
        _db.Employees.Update(employee);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Employee employee, CancellationToken ct)
    {
        _db.Employees.Remove(employee);
        await _db.SaveChangesAsync(ct);
    }
}
