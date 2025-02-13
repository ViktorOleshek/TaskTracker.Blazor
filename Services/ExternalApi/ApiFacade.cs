using Services.Abstraction;

namespace Services.ExternalApi;

public class ApiFacade : IApiFacade
{
    public IAuthService Auth { get; }
    public IUserService User { get; }

    public ApiFacade(IAuthService authService, IUserService userService)
    {
        Auth = authService;
        User = userService;
    }
}
