using System.ComponentModel.DataAnnotations;
using DataAccess.Models;

namespace BusinessLogic.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = default!;
    
    [Required]
    [StringLength(50)]
    public string Surname { get; set; } = default!;
    
    [Required]
    [StringLength(50)]
    public string Patronymic { get; set; } = default!;
    
    [Required]
    [StringLength(255)]
    [EmailAddress]
    public string Email { get; set; } = default!;
    
    public static EmployeeDto FromEntity(Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Surname = employee.Surname,
            Patronymic = employee.Patronymic,
            Email = employee.Email
        };
    }
}