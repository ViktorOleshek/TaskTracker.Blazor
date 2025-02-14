using Domain.DTOs;
using Domain.DTOs.Users.ChangePassword;
using Refit;

namespace Services.ExternalApi;

public interface IUserService
{
    [Post("/user/change-password")]
    Task<IApiResponse<GetUserDto>> ChangePasswordAsync([Body] ChangePasswordDTO model);
        
    [Post("/user/change-email")]
    Task<IApiResponse<GetUserDto>> ChangeEmailAsync([Body] string email);

    [Get("/user/current-user")]
    Task<IApiResponse<GetUserDto>> GetCurrentUserAsync();
}