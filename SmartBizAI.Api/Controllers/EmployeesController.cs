using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBizAI.Api.Services;
using SmartBizAI.Shared.DTOs;

namespace SmartBizAI.Api.Controllers;

[ApiController]
[Route("api/employees")]
public sealed class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Manager,Employee")]
    public async Task<ActionResult<List<EmployeeDto>>> GetAll(CancellationToken ct)
    {
        if (User.IsInRole("Employee"))
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrWhiteSpace(email))
            {
                return Forbid();
            }

            var self = await _service.GetByEmailAsync(email, ct);
            return self == null ? Ok(new List<EmployeeDto>()) : Ok(new List<EmployeeDto> { self });
        }

        var employees = await _service.GetAllAsync(ct);
        return Ok(employees);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Manager,Employee")]
    public async Task<ActionResult<EmployeeDto>> GetById(int id, CancellationToken ct)
    {
        var employee = await _service.GetByIdAsync(id, ct);
        if (employee == null)
        {
            return NotFound();
        }

        if (User.IsInRole("Employee"))
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (!string.Equals(employee.Email, email, StringComparison.OrdinalIgnoreCase))
            {
                return Forbid();
            }
        }

        return Ok(employee);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult<EmployeeDto>> Create(EmployeeDto dto, CancellationToken ct)
    {
        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Update(int id, EmployeeDto dto, CancellationToken ct)
    {
        var updated = await _service.UpdateAsync(id, dto, ct);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}
