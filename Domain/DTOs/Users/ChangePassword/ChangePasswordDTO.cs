namespace Domain.DTOs.Users.ChangePassword;

public class ChangePasswordDTO
{
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
}