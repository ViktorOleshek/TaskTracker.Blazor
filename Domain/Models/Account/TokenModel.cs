namespace Domain.Models.Account;

public class TokenModel
{
    public string Token { get; set; } = string.Empty;
    public double ExpiresIn { get; set; }
}