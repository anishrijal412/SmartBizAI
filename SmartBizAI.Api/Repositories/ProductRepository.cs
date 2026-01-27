using Microsoft.EntityFrameworkCore;
using SmartBizAI.Api.Data;
using SmartBizAI.Api.Entities;

namespace SmartBizAI.Api.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<List<Product>> GetAllAsync(CancellationToken ct)
        => _db.Products.AsNoTracking().OrderBy(p => p.Name).ToListAsync(ct);

    public Task<Product?> GetByIdAsync(int id, CancellationToken ct)
        => _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<Product> AddAsync(Product product, CancellationToken ct)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync(ct);
        return product;
    }

    public async Task UpdateAsync(Product product, CancellationToken ct)
    {
        _db.Products.Update(product);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Product product, CancellationToken ct)
    {
        _db.Products.Remove(product);
        await _db.SaveChangesAsync(ct);
    }
}
