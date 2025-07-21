using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs;

public class EmployeeUpdateDto : EmployeeCreateDto
{
    [Required]
    public int Id { get; set; }
}