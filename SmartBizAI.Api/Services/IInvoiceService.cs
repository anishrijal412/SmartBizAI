using SmartBizAI.Shared.DTOs;

namespace SmartBizAI.Api.Services;

public interface IInvoiceService
{
    Task<List<InvoiceDto>> GetAllAsync(CancellationToken ct);
    Task<InvoiceDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<InvoiceDto> CreateAsync(InvoiceDto dto, CancellationToken ct);
    Task<bool> UpdateAsync(int id, InvoiceDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
