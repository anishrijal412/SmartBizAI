using SmartBizAI.Api.Entities;
using SmartBizAI.Api.Repositories;
using SmartBizAI.Shared.DTOs;

namespace SmartBizAI.Api.Services;

public sealed class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _repo;

    public InvoiceService(IInvoiceRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<InvoiceDto>> GetAllAsync(CancellationToken ct)
    {
        var items = await _repo.GetAllAsync(ct);
        return items.Select(MapToDto).ToList();
    }

    public async Task<InvoiceDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        var item = await _repo.GetByIdAsync(id, ct);
        return item == null ? null : MapToDto(item);
    }

    public async Task<InvoiceDto> CreateAsync(InvoiceDto dto, CancellationToken ct)
    {
        var entity = new Invoice
        {
            InvoiceNumber = dto.InvoiceNumber,
            CustomerName = dto.CustomerName,
            Amount = dto.Amount,
            Status = dto.Status,
            IssueDate = dto.IssueDate,
            DueDate = dto.DueDate
        };

        var created = await _repo.AddAsync(entity, ct);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, InvoiceDto dto, CancellationToken ct)
    {
        var existing = await _repo.GetByIdAsync(id, ct);
        if (existing == null)
        {
            return false;
        }

        var entity = new Invoice
        {
            Id = id,
            InvoiceNumber = dto.InvoiceNumber,
            CustomerName = dto.CustomerName,
            Amount = dto.Amount,
            Status = dto.Status,
            IssueDate = dto.IssueDate,
            DueDate = dto.DueDate
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

    private static InvoiceDto MapToDto(Invoice entity)
        => new()
        {
            Id = entity.Id,
            InvoiceNumber = entity.InvoiceNumber,
            CustomerName = entity.CustomerName,
            Amount = entity.Amount,
            Status = entity.Status,
            IssueDate = entity.IssueDate,
            DueDate = entity.DueDate
        };
}
