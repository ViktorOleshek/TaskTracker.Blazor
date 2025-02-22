using Domain.DTOs.Projects;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Services.ExternalApi;

namespace UI.Components.Pages.Projects;
public partial class ProjectDetails
{
    [Parameter] public Guid ProjectId { get; set; }
    [Inject] private IApiFacade ApiFacade { get; set; } = default!;
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;

    private ProjectDetailResult? Project;

    protected override async Task OnInitializedAsync()
    {
        await LoadProjectDetails();
    }

    private async Task LoadProjectDetails()
    {
        var response = await ApiFacade.Project.GetProjectDetailAsync(ProjectId);
        if (response.IsSuccessStatusCode && response.Content is not null)
        {
            Project = response.Content;
        }
    }

    private void CloseDialog()
    {
        MudDialog.Close();
    }
}
