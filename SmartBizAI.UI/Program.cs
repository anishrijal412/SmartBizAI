using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SmartBizAI.UI.Auth;
using SmartBizAI.UI.Components;
using SmartBizAI.UI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<ITokenStore, TokenStore>();
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<ApiAuthenticationStateProvider>());

builder.Services.AddScoped<AuthTokenHandler>();
builder.Services.AddHttpClient<ApiClient>((sp, client) =>
    {
        var config = sp.GetRequiredService<IConfiguration>();
        var baseUrl = config["ApiBaseUrl"] ?? "https://localhost:5001/";
        client.BaseAddress = new Uri(baseUrl);
    })
    .AddHttpMessageHandler<AuthTokenHandler>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
