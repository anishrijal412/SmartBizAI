using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBizAI.Api.Services;
using SmartBizAI.Shared.DTOs;

namespace SmartBizAI.Api.Controllers;

[ApiController]
[Route("api/incomes")]
public sealed class IncomesController : ControllerBase
{
    private readonly IIncomeService _service;

    public IncomesController(IIncomeService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Accountant,Manager")]
    public async Task<ActionResult<List<IncomeDto>>> GetAll(CancellationToken ct)
    {
        var items = await _service.GetAllAsync(ct);
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Accountant,Manager")]
    public async Task<ActionResult<IncomeDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _service.GetByIdAsync(id, ct);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Accountant")]
    public async Task<ActionResult<IncomeDto>> Create(IncomeDto dto, CancellationToken ct)
    {
        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin,Accountant")]
    public async Task<IActionResult> Update(int id, IncomeDto dto, CancellationToken ct)
    {
        var updated = await _service.UpdateAsync(id, dto, ct);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin,Accountant")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}
