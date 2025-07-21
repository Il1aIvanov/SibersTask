using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs;

public class EmployeeCreateDto
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Surname { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Patronymic { get; set; } = null!;

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = null!;
}