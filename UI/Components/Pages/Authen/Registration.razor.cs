using Domain.Models.Account.Registration;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Services.ExternalApi;

namespace UI.Components.Pages.Authen;

public partial class Registration : ComponentBase
{
    RegisterModel model = new();
    RegisterModelValidation registerModelValidation = new();
    private string? errorMessage;
    private bool isSubmitting = false;
    private MudForm? registractionForm;

    [Inject]
    private IApiFacade ApiFacade { get; set; } = default!;
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    private async Task<bool> ValidateFormAsync()
    {
        if (registractionForm is not null)
        {
            await registractionForm.Validate();
            return registractionForm.IsValid;
        }
        return false;
    }
    private async Task HandleRegister()
    {
        errorMessage = null;

        if (!await ValidateFormAsync())
        {
            return;
        }

        isSubmitting = true;

        var response = await ApiFacade.Auth.RegisterAsync(model);

        if (response.IsSuccessStatusCode && response.Content is not null)
        {
            Console.WriteLine($"Successful registration! Token: {response.Content.Token.Token}");
            NavigationManager.NavigateTo("/profile");
        }
        else
        {
            errorMessage = "Invalid data, please try again.";
        }

        isSubmitting = false;
    }
}