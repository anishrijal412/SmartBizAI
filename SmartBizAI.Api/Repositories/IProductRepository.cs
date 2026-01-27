using SmartBizAI.Api.Entities;

namespace SmartBizAI.Api.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync(CancellationToken ct);
    Task<Product?> GetByIdAsync(int id, CancellationToken ct);
    Task<Product> AddAsync(Product product, CancellationToken ct);
    Task UpdateAsync(Product product, CancellationToken ct);
    Task DeleteAsync(Product product, CancellationToken ct);
}
