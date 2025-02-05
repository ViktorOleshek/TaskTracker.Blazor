namespace Domain.Models.Account;

public class LoginResponse
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public TokenModel Token { get; set; } = new TokenModel();
}