using Domain.Models.Account;
using Microsoft.AspNetCore.Components;

namespace UI.Components.Pages.Account;

public partial class Registration : ComponentBase
{
    RegisterModel model = new();
    RegisterModelValidation registerModelValidation = new();

    private string? errorMessage;
    private bool isSubmitting = false;

    private async Task HandleRegister()
    {
        isSubmitting = true;
        errorMessage = null;

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