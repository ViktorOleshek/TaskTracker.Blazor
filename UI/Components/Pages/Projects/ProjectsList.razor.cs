﻿using Domain.Models.Project;
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

    private async Task AddOrEditProject(ProjectModel? project)
    {
        var parameters = new DialogParameters
        {
            ["Project"] = project,
            ["OnProjectUpdated"] = EventCallback.Factory.Create<ProjectModel>(this, async (updatedProject) =>
            {
                UpdateProjectList(updatedProject);
                StateHasChanged();
            })
        };
        string title = project == null ? "Add project" : "Edit project";
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium };
        var dialog = await DialogService.ShowAsync<AddOrEditProject>(title, parameters, options);
    }
    private void UpdateProjectList(ProjectModel updatedProject)
    {
        if (updatedProject.ProjectId == null)
            return;

        var existingProject = projects.FirstOrDefault(p => p.ProjectId == updatedProject.ProjectId);

        if (existingProject == null)
        {
            projects.Add(updatedProject);
            return;
        }

        var index = projects.IndexOf(existingProject);
        projects [index] = updatedProject;
    }

    private void InfoProject(Guid projectId)
    {
        var parameters = new DialogParameters { ["ProjectId"] = projectId };
        DialogService.Show<ProjectDetails>("Project Details", parameters);
    }
}
