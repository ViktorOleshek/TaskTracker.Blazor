using Domain.DTOs;
using Domain.DTOs.Users.ChangePassword;
using Refit;

namespace Services.Abstraction;

public interface IUserService
{
    [Post("/user/change-password")]
    Task<ApiResponse<GetUserDto>> ChangePasswordAsync([Body] ChangePasswordDTO model);

    [Get("/user/current-user")]
    Task<ApiResponse<GetUserDto>> GetCurrentUserAsync();
}