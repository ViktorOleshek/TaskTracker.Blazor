using Domain.Models.Project;
using Domain.Models.Project.Update;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch;
using MudBlazor;
using Services.ExternalApi;
using System.Dynamic;

namespace UI.Components.Pages.Projects;

public partial class ProjectEdit
{
    [Parameter] public ProjectModel? Project { get; set; }
    [Inject] private IApiFacade ApiFacade { get; set; } = default!;
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;

    private ProjectModelValidation Validator = new();
    private MudForm? projectForm;
    private bool IsValid = false;
    private string? ErrorMessage;

    private async Task ValidateForm()
    {
        if (projectForm is not null)
        {
            await projectForm.Validate();
            IsValid = projectForm.IsValid;
        }
    }

    private async Task SaveProject()
    {
        await ValidateForm();
        if (!IsValid)
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
            MudDialog.Close(DialogResult.Ok(Project));
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