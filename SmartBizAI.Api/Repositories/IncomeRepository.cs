using Microsoft.EntityFrameworkCore;
using SmartBizAI.Api.Data;
using SmartBizAI.Api.Entities;

namespace SmartBizAI.Api.Repositories;

public sealed class IncomeRepository : IIncomeRepository
{
    private readonly AppDbContext _db;

    public IncomeRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<List<Income>> GetAllAsync(CancellationToken ct)
        => _db.Incomes.AsNoTracking().OrderByDescending(i => i.Date).ToListAsync(ct);

    public Task<Income?> GetByIdAsync(int id, CancellationToken ct)
        => _db.Incomes.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id, ct);

    public async Task<Income> AddAsync(Income income, CancellationToken ct)
    {
        _db.Incomes.Add(income);
        await _db.SaveChangesAsync(ct);
        return income;
    }

    public async Task UpdateAsync(Income income, CancellationToken ct)
    {
        _db.Incomes.Update(income);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Income income, CancellationToken ct)
    {
        _db.Incomes.Remove(income);
        await _db.SaveChangesAsync(ct);
    }
}
