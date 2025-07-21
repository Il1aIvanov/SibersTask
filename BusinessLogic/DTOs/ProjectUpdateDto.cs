using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs;

public class ProjectUpdateDto : ProjectCreateDto
{ 
    [Required]
    public int Id { get; set; }
}