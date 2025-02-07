using Domain.Models.Account;
using Microsoft.AspNetCore.Components;

namespace UI.Components.Pages.Account;

public partial class Login : ComponentBase
{
    private LoginModel loginModel = new LoginModel();
    private LoginModelValidation loginModelValidation = new LoginModelValidation();
    private string? errorMessage;
    private bool isSubmitting = false;

    private async Task HandleLogin()
    {
        isSubmitting = true;
        errorMessage = null;

        var response = await AuthService.LoginAsync(loginModel);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Login successful!");
            NavigationManager.NavigateTo("/dashboard");
        }
        else
        {
            errorMessage = "Invalid data, please try again.";
        }

        isSubmitting = false;
    }
}