using Domain.DTOs.Users.ChangePassword;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Services.ExternalApi;

namespace UI.Components.Pages.Account;

public partial class ChangePasswordDialog
{
    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = default!;

    [Inject]
    private IApiFacade ApiFacade { get; set; } = default!;

    private ChangePasswordDTO changePasswordModel = new();

    private async Task Submit()
    {
        var response = await ApiFacade.User.ChangePasswordAsync(changePasswordModel);
        if (response.IsSuccessStatusCode)
        {
            MudDialog?.Close(DialogResult.Ok(true));
            return;
        }

        MudDialog?.Close(DialogResult.Ok(false));
    }

    private void Cancel() => MudDialog?.Cancel();
}
