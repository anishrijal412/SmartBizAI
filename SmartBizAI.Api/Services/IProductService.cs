using SmartBizAI.Shared.DTOs;

namespace SmartBizAI.Api.Services;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync(CancellationToken ct);
    Task<ProductDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<ProductDto> CreateAsync(ProductDto dto, CancellationToken ct);
    Task<bool> UpdateAsync(int id, ProductDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
