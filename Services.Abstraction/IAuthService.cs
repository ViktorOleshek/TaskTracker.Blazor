using Domain.Models.Account;
using Refit;

namespace Services.Abstraction;

public interface IAuthService
{
    [Post("/user/login")]
    Task<ApiResponse<LoginResponse>> LoginAsync([Body] LoginModel model);

    [Post("/user/register")]
    Task<ApiResponse<RegisterResponse>> RegisterAsync([Body] RegisterModel model);
}