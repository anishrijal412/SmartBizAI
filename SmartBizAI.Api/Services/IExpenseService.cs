using SmartBizAI.Shared.DTOs;

namespace SmartBizAI.Api.Services;

public interface IExpenseService
{
    Task<List<ExpenseDto>> GetAllAsync(CancellationToken ct);
    Task<ExpenseDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<ExpenseDto> CreateAsync(ExpenseDto dto, CancellationToken ct);
    Task<bool> UpdateAsync(int id, ExpenseDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
