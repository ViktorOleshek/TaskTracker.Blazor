using Domain.DTOs.Projects;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Services.ExternalApi;

namespace UI.Components.Pages.Projects;

public partial class ProjectsList
{
    [Inject] private IApiFacade ApiFacade { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;

    private List<GetUserProjectsResult> projects = new();

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
        // Логіка відкриття діалогу для створення нового проєкту
    }

    private async Task EditProject(GetUserProjectsResult project)
    {
        // Логіка відкриття діалогу редагування проєкту
    }

    private void InfoProject(Guid projectId)
    {
        var parameters = new DialogParameters { ["ProjectId"] = projectId };
        DialogService.Show<ProjectDetails>("Project Details", parameters);
    }
}
