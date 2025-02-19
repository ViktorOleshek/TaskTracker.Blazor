namespace Domain.Models.Project;

public class ProjectModel
{
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
}