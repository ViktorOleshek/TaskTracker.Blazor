using Domain.Models.Project;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Services.ExternalApi;

namespace UI.Components.Pages.Projects;

public partial class ProjectsList
{
    [Inject] private IApiFacade ApiFacade { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;

    private List<ProjectModel> projects = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadProjects();
    }

    private async Task LoadProjects()
    {
        var response = await ApiFacade.Project.GetUserProjectsAsync();
        if (response.IsSuccessStatusCode && response.Content is not null)
        {
            projects = response.Content.ToList();
        }
    }

    private async Task DeleteProject(Guid projectId)
    {
        bool? dialog = await DialogService.ShowMessageBox(
            title: "Delete Project",
            message: "Are you sure you want to delete this project?",
            yesText: "Yes",
            noText: "No"
        );

        if (dialog == true)
        {
            var response = await ApiFacade.Project.DeleteProjectAsync(projectId);
            if (response.IsSuccessStatusCode)
            {
                projects.RemoveAll(p => p.ProjectId == projectId);
            }
        }
    }

    private async Task OpenAddProjectDialog()
    {
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium };
        var dialog = DialogService.Show<ProjectEdit>("Add Project", options);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await LoadProjects();
        }
    }

    private async Task EditProject(ProjectModel project)
    {
        var parameters = new DialogParameters { ["Project"] = project };
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium };

        var dialog = DialogService.Show<ProjectEdit>("Edit Project", parameters, options);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await LoadProjects();
        }
    }

    private void InfoProject(Guid projectId)
    {
        var parameters = new DialogParameters { ["ProjectId"] = projectId };
        DialogService.Show<ProjectDetails>("Project Details", parameters);
    }
}
