using Domain.DTOs.Projects;
using Domain.Models.Project;
using Microsoft.AspNetCore.JsonPatch;
using Refit;

namespace Services.ExternalApi;

public interface IProjectService
{
    [Post("/project")]
    Task<IApiResponse<Guid>> CreateProjectAsync([Body] ProjectModel dto);

    [Get("/project/user-projects")]
    Task<IApiResponse<IEnumerable<ProjectModel>>> GetUserProjectsAsync();

    [Get("/project/{projectId}")]
    Task<IApiResponse<ProjectDetailResult>> GetProjectDetailAsync(Guid projectId);

    [Delete("/project/{projectId}")]
    Task<IApiResponse> DeleteProjectAsync(Guid projectId);

    [Patch("/project/{projectId}")]
    Task<IApiResponse> UpdateProjectAsync(Guid projectId, [Body] JsonPatchDocument<ProjectModel> patchDoc);
}