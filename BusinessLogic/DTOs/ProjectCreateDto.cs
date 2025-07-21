using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs;

public class ProjectCreateDto
{
    [Required]
    [StringLength(128)]
    public string ProjectName { get; set; } = null!;

    [Required]
    [StringLength(128)]
    public string CustomerCompanyName { get; set; } = null!;

    [Required]
    [StringLength(128)]
    public string ExecutorCompanyName { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    [Range(1, 10)]
    public int Priority { get; set; }

    [Required]
    public int ProjectManagerId { get; set; }
}