using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("Project")]
public class ProjectController(IProjectService projectService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProjectsAsync()
    {
        await projectService.GetAllAsync();
        return Ok();
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProjectAsync([FromRoute]  int id)
    {
        var projectDto = await projectService.GetByIdAsync(id);
        if (projectDto == null) 
            return NotFound();
        return Ok(projectDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProjectAsync([FromBody] ProjectCreateDto projectCreateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await projectService.CreateAsync(projectCreateDto);
        return NoContent();
    }
    
    [HttpPost("{projectId}/add-employee/{employeeId}")]
    public async Task<IActionResult> AddEmployeeAsync([FromRoute] int projectId, [FromRoute] int employeeId, CancellationToken cancellationToken)
    {
        await projectService.AddEmployeeAsync(projectId, employeeId, cancellationToken);
        return NoContent();
    }
    
    [HttpPost("filter")]
    public async Task<IActionResult> FilterProjectsAsync([FromBody] ProjectFilterDto filterDto, CancellationToken cancellationToken)
    {
        var result = await projectService.FilterAsync(filterDto, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProjectAsync(
        [FromRoute] int id,
        [FromBody] ProjectUpdateDto dto)
    {
        if (id != dto.Id)
            return BadRequest();
        
        await projectService.UpdateAsync(dto);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProjectAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        await projectService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{projectId}/remove-employee/{employeeId}")]
    public async Task<IActionResult> RemoveEmployeeAsync([FromRoute] int projectId, [FromRoute] int employeeId, CancellationToken cancellationToken)
    {
        await projectService.RemoveEmployeeAsync(projectId, employeeId, cancellationToken);
        return NoContent();
    }
}