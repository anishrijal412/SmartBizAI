using Microsoft.EntityFrameworkCore;
using SmartBizAI.Api.Data;
using SmartBizAI.Api.Entities;

namespace SmartBizAI.Api.Repositories;

public sealed class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDbContext _db;

    public InvoiceRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<List<Invoice>> GetAllAsync(CancellationToken ct)
        => _db.Invoices.AsNoTracking().OrderByDescending(i => i.IssueDate).ToListAsync(ct);

    public Task<Invoice?> GetByIdAsync(int id, CancellationToken ct)
        => _db.Invoices.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id, ct);

    public async Task<Invoice> AddAsync(Invoice invoice, CancellationToken ct)
    {
        _db.Invoices.Add(invoice);
        await _db.SaveChangesAsync(ct);
        return invoice;
    }

    public async Task UpdateAsync(Invoice invoice, CancellationToken ct)
    {
        _db.Invoices.Update(invoice);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Invoice invoice, CancellationToken ct)
    {
        _db.Invoices.Remove(invoice);
        await _db.SaveChangesAsync(ct);
    }
}
