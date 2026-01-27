using SmartBizAI.Api.Entities;
using SmartBizAI.Api.Repositories;
using SmartBizAI.Shared.DTOs;

namespace SmartBizAI.Api.Services;

public sealed class ProductService : IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ProductDto>> GetAllAsync(CancellationToken ct)
    {
        var items = await _repo.GetAllAsync(ct);
        return items.Select(MapToDto).ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        var item = await _repo.GetByIdAsync(id, ct);
        return item == null ? null : MapToDto(item);
    }

    public async Task<ProductDto> CreateAsync(ProductDto dto, CancellationToken ct)
    {
        var entity = new Product
        {
            Name = dto.Name,
            SKU = dto.SKU,
            UnitPrice = dto.UnitPrice,
            Quantity = dto.Quantity,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repo.AddAsync(entity, ct);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, ProductDto dto, CancellationToken ct)
    {
        var existing = await _repo.GetByIdAsync(id, ct);
        if (existing == null)
        {
            return false;
        }

        var entity = new Product
        {
            Id = id,
            Name = dto.Name,
            SKU = dto.SKU,
            UnitPrice = dto.UnitPrice,
            Quantity = dto.Quantity,
            CreatedAt = existing.CreatedAt
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

    private static ProductDto MapToDto(Product entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            SKU = entity.SKU,
            UnitPrice = entity.UnitPrice,
            Quantity = entity.Quantity,
            CreatedAt = entity.CreatedAt
        };
}
