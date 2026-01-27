using Microsoft.EntityFrameworkCore;
using SmartBizAI.Api.Data;
using SmartBizAI.Api.Entities;

namespace SmartBizAI.Api.Repositories;

public sealed class ExpenseRepository : IExpenseRepository
{
    private readonly AppDbContext _db;

    public ExpenseRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<List<Expense>> GetAllAsync(CancellationToken ct)
        => _db.Expenses.AsNoTracking().OrderByDescending(e => e.Date).ToListAsync(ct);

    public Task<Expense?> GetByIdAsync(int id, CancellationToken ct)
        => _db.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, ct);

    public async Task<Expense> AddAsync(Expense expense, CancellationToken ct)
    {
        _db.Expenses.Add(expense);
        await _db.SaveChangesAsync(ct);
        return expense;
    }

    public async Task UpdateAsync(Expense expense, CancellationToken ct)
    {
        _db.Expenses.Update(expense);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Expense expense, CancellationToken ct)
    {
        _db.Expenses.Remove(expense);
        await _db.SaveChangesAsync(ct);
    }
}
