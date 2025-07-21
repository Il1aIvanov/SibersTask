using System.ComponentModel.DataAnnotations;
using DataAccess.Models;

namespace BusinessLogic.DTOs;

public class ProjectDto
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(128)]
    public string ProjectName { get; set; } = default!;
    
    [Required]
    [StringLength(128)]
    public string CustomerCompanyName { get; set; } = default!;
    
    [Required]
    [StringLength(128)]
    public string ExecutorCompanyName { get; set; } = default!;
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    [Required]
    [Range(1,10)]
    public int Priority { get; set; }

    [Required]
    public EmployeeDto ProjectManager { get; set; } = default!;

    public List<EmployeeDto> Employees { get; set; } = new();
    
    public static ProjectDto FromEntity(Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            ProjectName = project.ProjectName!,
            CustomerCompanyName = project.CustomerCompanyName!,
            ExecutorCompanyName = project.ExecutorCompanyName!,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Priority = project.Priority,
            ProjectManager = EmployeeDto.FromEntity(project.ProjectManager!),
            Employees = project.Employees?.Select(EmployeeDto.FromEntity).ToList() ?? new()
        };
    }
}