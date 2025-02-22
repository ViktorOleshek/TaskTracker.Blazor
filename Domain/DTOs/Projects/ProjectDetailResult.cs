namespace Domain.DTOs.Projects;

public class ProjectDetailResult
{
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string ProjectDescription { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public string OwnerName { get; set; }
    public int MemberCount { get; set; }
}