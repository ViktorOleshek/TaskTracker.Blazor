using Domain.Models.Project;
using Domain.Models.Project.Update;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch;
using MudBlazor;
using Services.ExternalApi;

namespace UI.Components.Pages.Projects;

public partial class ProjectEdit
{
    [Parameter] public ProjectModel? Project { get; set; }
    [Parameter] public EventCallback<ProjectModel> OnProjectUpdated { get; set; }
    [Inject] private IApiFacade ApiFacade { get; set; } = default!;
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;

    private ProjectModelValidation Validator = new();
    private MudForm? projectForm;
    private string? ErrorMessage = null;

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

        var patchDoc = new JsonPatchDocument<ProjectModel>();
        patchDoc.Replace(p => p.ProjectName, Project.ProjectName);
        patchDoc.Replace(p => p.ProjectDescription, Project.ProjectDescription);
        patchDoc.Replace(p => p.StartDate, Project.StartDate);
        patchDoc.Replace(p => p.FinishDate, Project.FinishDate);

        var updateResponse = await ApiFacade.Project.UpdateProjectAsync(Project.ProjectId, patchDoc);
        if (updateResponse.IsSuccessStatusCode)
        {
            await OnProjectUpdated.InvokeAsync(Project);
            MudDialog.Close();
        }
        else
        {
            ErrorMessage = "Failed to update project. Please try again.";
        }
    }

    private void CloseDialog()
    {
        MudDialog.Close();
    }
}