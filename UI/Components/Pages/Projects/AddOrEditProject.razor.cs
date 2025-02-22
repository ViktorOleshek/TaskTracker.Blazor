using Domain.Models.Project;
using Domain.Models.Project.Update;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch;
using MudBlazor;
using Refit;
using Services.ExternalApi;

namespace UI.Components.Pages.Projects;

public partial class AddOrEditProject
{
    [Parameter] public ProjectModel? Project { get; set; }
    [Parameter] public EventCallback<ProjectModel> OnProjectUpdated { get; set; }
    [Inject] private IApiFacade ApiFacade { get; set; } = default!;
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;

    private ProjectModel? originalProject;
    private ProjectModelValidation Validator = new();
    private MudForm? projectForm;
    private string? ErrorMessage = null;

    protected override async Task OnInitializedAsync()
    {
        Project ??= new ProjectModel();
        originalProject = new ProjectModel
        {
            ProjectId = Project.ProjectId,
            ProjectName = Project.ProjectName,
            ProjectDescription = Project.ProjectDescription,
            StartDate = Project.StartDate,
            FinishDate = Project.FinishDate
        };
    }
    private async Task<bool> ValidateFormAsync()
    {
        if (projectForm is not null)
        {
            await projectForm.Validate();
            return projectForm.IsValid;
        }

        return false;
    }

    private async Task SaveProject()
    {
        ErrorMessage = null;

        if (!await ValidateFormAsync())
        {
            ErrorMessage = "Please fix validation errors before saving.";
            return;
        }

        var response = Project.ProjectId == null
            ? await ApiFacade.Project.CreateProjectAsync(Project)
            : await UpdateProjectAsync();

        if (response?.IsSuccessStatusCode == true)
        {
            if (Project.ProjectId == null && response is IApiResponse<Guid> createResponse)
            {
                Project.ProjectId = createResponse.Content;
            }

            await OnProjectUpdated.InvokeAsync(Project);
            MudDialog.Close();
        }
        else
        {
            ErrorMessage = "Failed to save project. Please try again.";
        }
    }

    private async Task<IApiResponse?> UpdateProjectAsync()
    {
        var patchDoc = new JsonPatchDocument<ProjectModel>();

        if (Project.ProjectName != originalProject.ProjectName)
        {
            patchDoc.Replace(p => p.ProjectName, Project.ProjectName);
        }

        if (Project.ProjectDescription != originalProject.ProjectDescription)
        {
            patchDoc.Replace(p => p.ProjectDescription, Project.ProjectDescription);
        }

        if (Project.StartDate != originalProject.StartDate)
        {
            patchDoc.Replace(p => p.StartDate, Project.StartDate);
        }

        if (Project.FinishDate != originalProject.FinishDate)
        {
            patchDoc.Replace(p => p.FinishDate, Project.FinishDate);
        }

        return patchDoc.Operations.Any()
            ? await ApiFacade.Project.UpdateProjectAsync(Project.ProjectId!.Value, patchDoc)
            : null;
    }

    private void CloseDialog()
    {
        MudDialog.Close();
    }
}