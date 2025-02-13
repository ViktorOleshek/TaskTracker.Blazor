using Services.Abstraction;

namespace Services.ExternalApi;

public interface IApiFacade
{
    IAuthService Auth { get; }
    IUserService User { get; }
}