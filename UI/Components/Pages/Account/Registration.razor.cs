using Domain.Models.Account;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace UI.Components.Pages.Account;

public partial class Registration : ComponentBase
{
    RegisterModel model = new();
    RegisterModelValidation registerModelValidation = new();
    private string? errorMessage;
    private bool isSubmitting = false;
    private MudForm? registractionForm;

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

        var response = await AuthService.RegisterAsync(model);

        if (response.IsSuccessStatusCode && response.Content is not null)
        {
            Console.WriteLine($"Successful registration! Token: {response.Content.Token.Token}");
            NavigationManager.NavigateTo("/dashboard");
        }
        else
        {
            errorMessage = "Invalid data, please try again.";
        }

        isSubmitting = false;
    }
}