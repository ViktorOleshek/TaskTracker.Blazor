using Domain.DTOs;
using Domain.Models.Account;
using Refit;

namespace Services.ExternalApi;

public interface IAuthService
{
    [Post("/identity/login")]
    Task<ApiResponse<LoginResponse>> LoginAsync([Body] LoginModel model);

    [Post("/identity/register")]
    Task<ApiResponse<RegisterResponse>> RegisterAsync([Body] RegisterModel model);

    [Get("/user/current-user")]
    Task<ApiResponse<UserDto>> GetCurrentUserAsync();
}