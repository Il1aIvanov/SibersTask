using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("Employee")]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllEmployeesAsync()
    {
        await employeeService.GetAllAsync();
        return Ok();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEmployeeAsync([FromRoute] int id)
    {
        var employeeDto = await employeeService.GetByIdAsync(id);
        if (employeeDto == null) 
            return NotFound();
        
        return Ok(employeeDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployeeAsync([FromBody] EmployeeCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await employeeService.CreateAsync(dto);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEmployeeAsync([FromRoute] int id, [FromBody] EmployeeUpdateDto dto)
    {
        if (id != dto.Id)
            return BadRequest();
        await employeeService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployeeAsync([FromRoute] int id)
    {
        await employeeService.DeleteAsync(id);
        return NoContent();
    }
}