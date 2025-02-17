using Domain.DTOs.Projects;
using Refit;

namespace Services.ExternalApi;

public interface IProjectService
{
    [Post("/project")]
    Task<IApiResponse<Guid>> CreateProjectAsync([Body] CreateProjectDTO dto);

    [Get("/project/user-projects")]
    Task<IApiResponse<IEnumerable<GetUserProjectsResult>>> GetUserProjectsAsync();

    [Get("/project/{projectId}")]
    Task<IApiResponse<GetProjectDetailResult>> GetProjectDetailAsync(Guid projectId);

    [Delete("/project/{projectId}")]
    Task<IApiResponse> DeleteProjectAsync(Guid projectId);
}