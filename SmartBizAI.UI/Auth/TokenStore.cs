using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace SmartBizAI.UI.Auth;

public interface ITokenStore
{
    Task<string?> GetTokenAsync();
    Task SetTokenAsync(string token);
    Task ClearAsync();
}

public sealed class TokenStore : ITokenStore
{
    private const string TokenKey = "sbai.auth.token";
    private readonly ProtectedLocalStorage _storage;

    public TokenStore(ProtectedLocalStorage storage)
    {
        _storage = storage;
    }

    public async Task<string?> GetTokenAsync()
    {
        var result = await _storage.GetAsync<string>(TokenKey);
        return result.Success ? result.Value : null;
    }

    public async Task SetTokenAsync(string token)
    {
        await _storage.SetAsync(TokenKey, token);
    }

    public async Task ClearAsync()
    {
        await _storage.DeleteAsync(TokenKey);
    }
}
