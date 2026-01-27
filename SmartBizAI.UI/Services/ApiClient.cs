using System.Net.Http.Json;
using SmartBizAI.Shared.DTOs;
using SmartBizAI.Shared.DTOs.Ai;
using SmartBizAI.Shared.DTOs.Auth;
using SmartBizAI.Shared.DTOs.Reports;

namespace SmartBizAI.UI.Services;

public sealed class ApiClient
{
    private readonly HttpClient _http;

    public ApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken ct)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", request, ct);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<LoginResponseDto>(cancellationToken: ct);
    }

    public Task<UserInfoDto?> GetMeAsync(CancellationToken ct)
        => _http.GetFromJsonAsync<UserInfoDto>("api/auth/me", ct);

    public Task<List<EmployeeDto>?> GetEmployeesAsync(CancellationToken ct)
        => _http.GetFromJsonAsync<List<EmployeeDto>>("api/employees", ct);

    public Task<List<ProductDto>?> GetProductsAsync(CancellationToken ct)
        => _http.GetFromJsonAsync<List<ProductDto>>("api/products", ct);

    public Task<List<IncomeDto>?> GetIncomesAsync(CancellationToken ct)
        => _http.GetFromJsonAsync<List<IncomeDto>>("api/incomes", ct);

    public Task<List<ExpenseDto>?> GetExpensesAsync(CancellationToken ct)
        => _http.GetFromJsonAsync<List<ExpenseDto>>("api/expenses", ct);

    public Task<List<InvoiceDto>?> GetInvoicesAsync(CancellationToken ct)
        => _http.GetFromJsonAsync<List<InvoiceDto>>("api/invoices", ct);

    public async Task<bool> CreateEmployeeAsync(EmployeeDto dto, CancellationToken ct)
    {
        var response = await _http.PostAsJsonAsync("api/employees", dto, ct);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateProductAsync(ProductDto dto, CancellationToken ct)
    {
        var response = await _http.PostAsJsonAsync("api/products", dto, ct);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateIncomeAsync(IncomeDto dto, CancellationToken ct)
    {
        var response = await _http.PostAsJsonAsync("api/incomes", dto, ct);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateExpenseAsync(ExpenseDto dto, CancellationToken ct)
    {
        var response = await _http.PostAsJsonAsync("api/expenses", dto, ct);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateInvoiceAsync(InvoiceDto dto, CancellationToken ct)
    {
        var response = await _http.PostAsJsonAsync("api/invoices", dto, ct);
        return response.IsSuccessStatusCode;
    }

    public Task<SummaryReportDto?> GetSummaryAsync(int month, int year, CancellationToken ct)
        => _http.GetFromJsonAsync<SummaryReportDto>($"api/reports/summary?month={month}&year={year}", ct);

    public Task<List<MonthlyReportItemDto>?> GetMonthlyAsync(int year, CancellationToken ct)
        => _http.GetFromJsonAsync<List<MonthlyReportItemDto>>($"api/reports/monthly?year={year}", ct);

    public async Task<AiChatResponseDto?> SendChatAsync(AiChatRequestDto request, CancellationToken ct)
    {
        var response = await _http.PostAsJsonAsync("api/ai/chat", request, ct);
        if (!response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<AiChatResponseDto>(cancellationToken: ct);
        }
        return await response.Content.ReadFromJsonAsync<AiChatResponseDto>(cancellationToken: ct);
    }
}
