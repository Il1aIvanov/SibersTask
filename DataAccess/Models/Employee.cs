using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Employee
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }
    
    [Required]
    [StringLength(50)]
    public string? Surname { get; set; }
    
    [Required]
    [StringLength(50)]
    public string? Patronymic { get; set; }
    
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string? Email { get; set; }
    
    public ICollection<Project>? Projects { get; set; }
    public ICollection<Project>? ManagedProjects { get; set; }
}