using Microsoft.AspNetCore.Components;
using MudBlazor;
using Services.ExternalApi;

namespace UI.Components.Pages.Account;

public partial class ChangeEmailDialog
{
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = default!;

    [Inject]
    public required IApiFacade ApiFacade { get; set; }

    private string newEmail = string.Empty;

    private async Task Submit()
    {
        var response = await ApiFacade.User.ChangeEmailAsync(newEmail);
        if (response.IsSuccessStatusCode)
        {
            MudDialog?.Close(DialogResult.Ok(true));
        }

        MudDialog?.Close(DialogResult.Ok(false));
    }

    private void Cancel() => MudDialog?.Cancel();
}
