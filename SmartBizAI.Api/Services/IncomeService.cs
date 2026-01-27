using SmartBizAI.Api.Entities;
using SmartBizAI.Api.Repositories;
using SmartBizAI.Shared.DTOs;

namespace SmartBizAI.Api.Services;

public sealed class IncomeService : IIncomeService
{
    private readonly IIncomeRepository _repo;

    public IncomeService(IIncomeRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<IncomeDto>> GetAllAsync(CancellationToken ct)
    {
        var items = await _repo.GetAllAsync(ct);
        return items.Select(MapToDto).ToList();
    }

    public async Task<IncomeDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        var item = await _repo.GetByIdAsync(id, ct);
        return item == null ? null : MapToDto(item);
    }

    public async Task<IncomeDto> CreateAsync(IncomeDto dto, CancellationToken ct)
    {
        var entity = new Income
        {
            Amount = dto.Amount,
            Source = dto.Source,
            Date = dto.Date,
            Notes = dto.Notes
        };

        var created = await _repo.AddAsync(entity, ct);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, IncomeDto dto, CancellationToken ct)
    {
        var existing = await _repo.GetByIdAsync(id, ct);
        if (existing == null)
        {
            return false;
        }

        var entity = new Income
        {
            Id = id,
            Amount = dto.Amount,
            Source = dto.Source,
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

    private static IncomeDto MapToDto(Income entity)
        => new()
        {
            Id = entity.Id,
            Amount = entity.Amount,
            Source = entity.Source,
            Date = entity.Date,
            Notes = entity.Notes
        };
}
