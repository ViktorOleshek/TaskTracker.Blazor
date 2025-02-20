namespace Domain.Models.Project;

public class ProjectModel
{
    public Guid? ProjectId { get; set; } = null;
    public string ProjectName { get; set; } = string.Empty;
    public string? ProjectDescription { get; set; } = null;
    public DateTime? StartDate { get; set; } = null;
    public DateTime? FinishDate { get; set; } = null;
}