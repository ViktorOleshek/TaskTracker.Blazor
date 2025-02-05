namespace Domain.Models.Account;

public class RegisterResponse
{
    public Guid UserId { get; set; }
    public TokenModel Token { get; set; } = new TokenModel();
}