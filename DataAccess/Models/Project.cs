using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Project
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(128)]
    public string? ProjectName { get; set; }
    
    [Required]
    [StringLength(128)]
    public string? CustomerCompanyName { get; set; }
    
    [Required]
    [StringLength(128)]
    public string? ExecutorCompanyName { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    [Required]
    public int Priority { get; set; }
    
    [Required]
    public int ProjectManagerId { get; set; }
    
    public Employee? ProjectManager { get; set; }
    
    public ICollection<Employee>? Employees { get; set; }
    
}