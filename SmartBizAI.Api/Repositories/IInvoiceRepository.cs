using SmartBizAI.Api.Entities;

namespace SmartBizAI.Api.Repositories;

public interface IInvoiceRepository
{
    Task<List<Invoice>> GetAllAsync(CancellationToken ct);
    Task<Invoice?> GetByIdAsync(int id, CancellationToken ct);
    Task<Invoice> AddAsync(Invoice invoice, CancellationToken ct);
    Task UpdateAsync(Invoice invoice, CancellationToken ct);
    Task DeleteAsync(Invoice invoice, CancellationToken ct);
}
