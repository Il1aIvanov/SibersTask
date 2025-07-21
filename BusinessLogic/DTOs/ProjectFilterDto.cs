using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs;

public class ProjectFilterDto
{
    [DataType(DataType.Date)]
    public DateTime? StartDateFrom { get; set; }

    [DataType(DataType.Date)]
    public DateTime? StartDateTo { get; set; }

    [Range(1, 10)]
    public int? MinPriority { get; set; }

    [Range(1, 10)]
    public int? MaxPriority { get; set; }

    [StringLength(128)]
    public string? ProjectNameContains { get; set; }

    [RegularExpression("^(ProjectName|StartDate|EndDate|Priority)$", 
        ErrorMessage = "Valid sorting fields are: ProjectName, StartDate, EndDate, Priority")]
    public string? OrderBy { get; set; }

    public bool OrderDescending { get; set; } = false;
}