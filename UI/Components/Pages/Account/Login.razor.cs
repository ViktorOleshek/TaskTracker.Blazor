﻿using Domain.Models.Account;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace UI.Components.Pages.Account;

public partial class Login : ComponentBase
{
    private LoginModel loginModel = new LoginModel();
    private LoginModelValidation loginModelValidation = new LoginModelValidation();
    private string? errorMessage;
    private bool isSubmitting = false;
    private MudForm? loginForm;

    private async Task<bool> ValidateFormAsync()
    {
        if (loginForm is not null)
        {
            await loginForm.Validate();
            return loginForm.IsValid;
        }
        return false;
    }
    private async Task HandleLogin()
    {
        errorMessage = null;

        if (!await ValidateFormAsync())
        {
            return;
        }

        isSubmitting = true;

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
