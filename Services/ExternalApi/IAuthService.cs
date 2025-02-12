using Domain.Models.Account.Login;
using Domain.Models.Account.Registration;
using Refit;

namespace Services.ExternalApi;

public interface IAuthService
{
    [Post("/identity/login")]
    Task<ApiResponse<LoginResponse>> LoginAsync([Body] LoginModel model);

    [Post("/identity/register")]
    Task<ApiResponse<RegisterResponse>> RegisterAsync([Body] RegisterModel model);
}