namespace Domain.DTOs.Projects;

public class GetUserProjectsResult
{
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
}