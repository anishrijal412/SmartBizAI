using SmartBizAI.Api.Entities;

namespace SmartBizAI.Api.Repositories;

public interface IExpenseRepository
{
    Task<List<Expense>> GetAllAsync(CancellationToken ct);
    Task<Expense?> GetByIdAsync(int id, CancellationToken ct);
    Task<Expense> AddAsync(Expense expense, CancellationToken ct);
    Task UpdateAsync(Expense expense, CancellationToken ct);
    Task DeleteAsync(Expense expense, CancellationToken ct);
}
