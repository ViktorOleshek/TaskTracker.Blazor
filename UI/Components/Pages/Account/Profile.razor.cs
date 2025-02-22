using Domain.DTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Services.ExternalApi;

namespace UI.Components.Pages.Account;

public partial class Profile
{
    [Inject] private IApiFacade ApiFacade { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;

    private GetUserDto? user;
    private string? errorMessage;

    private readonly DialogOptions defaultOptions = new()
    {
        CloseButton = true,
        CloseOnEscapeKey = true,
        MaxWidth = MaxWidth.ExtraSmall,
        FullWidth = true
    };

    protected override async Task OnInitializedAsync()
    {
        await LoadUserData();
    }

    private async Task LoadUserData()
    {
        var response = await ApiFacade.User.GetCurrentUserAsync();
        if (response.IsSuccessStatusCode && response.Content is not null)
        {
            user = response.Content;
        }
    }

    private async Task ShowDialog<TDialog>(string title) where TDialog : ComponentBase
    {
        errorMessage = null;

        var type = title.ToLower();
        var dialog = await DialogService.ShowAsync<TDialog>("", defaultOptions);
        var result = await dialog.Result;

        if (result.Canceled)
        {
            return;
        }

        if (result.Data is bool success && success)
        {
            await DialogService.ShowMessageBox(
                "Success",
                $"{title} changed successfully!",
                yesText: "OK");

            if (title == "Email")
            {
                await LoadUserData();
            }

            return;
        }

        errorMessage = $"Failed to change {type}";
    }
}