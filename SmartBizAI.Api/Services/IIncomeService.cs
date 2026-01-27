using SmartBizAI.Shared.DTOs;

namespace SmartBizAI.Api.Services;

public interface IIncomeService
{
    Task<List<IncomeDto>> GetAllAsync(CancellationToken ct);
    Task<IncomeDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<IncomeDto> CreateAsync(IncomeDto dto, CancellationToken ct);
    Task<bool> UpdateAsync(int id, IncomeDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
