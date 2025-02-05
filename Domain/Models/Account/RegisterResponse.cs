namespace Domain.Models.Account;

public class RegisterResponse
{
    public Guid Id { get; set; }
    public TokenModel Token { get; set; } = new TokenModel();
}