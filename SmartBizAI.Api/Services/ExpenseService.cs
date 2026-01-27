using SmartBizAI.Api.Entities;
using SmartBizAI.Api.Repositories;
using SmartBizAI.Shared.DTOs;

namespace SmartBizAI.Api.Services;

public sealed class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _repo;

    public ExpenseService(IExpenseRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ExpenseDto>> GetAllAsync(CancellationToken ct)
    {
        var items = await _repo.GetAllAsync(ct);
        return items.Select(MapToDto).ToList();
    }

    public async Task<ExpenseDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        var item = await _repo.GetByIdAsync(id, ct);
        return item == null ? null : MapToDto(item);
    }

    public async Task<ExpenseDto> CreateAsync(ExpenseDto dto, CancellationToken ct)
    {
        var entity = new Expense
        {
            Amount = dto.Amount,
            Category = dto.Category,
            Date = dto.Date,
            Notes = dto.Notes
        };

        var created = await _repo.AddAsync(entity, ct);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, ExpenseDto dto, CancellationToken ct)
    {
        var existing = await _repo.GetByIdAsync(id, ct);
        if (existing == null)
        {
            return false;
        }

        var entity = new Expense
        {
            Id = id,
            Amount = dto.Amount,
            Category = dto.Category,
            Date = dto.Date,
            Notes = dto.Notes
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

    private static ExpenseDto MapToDto(Expense entity)
        => new()
        {
            Id = entity.Id,
            Amount = entity.Amount,
            Category = entity.Category,
            Date = entity.Date,
            Notes = entity.Notes
        };
}
