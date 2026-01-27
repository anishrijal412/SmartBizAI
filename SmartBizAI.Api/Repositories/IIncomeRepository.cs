using SmartBizAI.Api.Entities;

namespace SmartBizAI.Api.Repositories;

public interface IIncomeRepository
{
    Task<List<Income>> GetAllAsync(CancellationToken ct);
    Task<Income?> GetByIdAsync(int id, CancellationToken ct);
    Task<Income> AddAsync(Income income, CancellationToken ct);
    Task UpdateAsync(Income income, CancellationToken ct);
    Task DeleteAsync(Income income, CancellationToken ct);
}
