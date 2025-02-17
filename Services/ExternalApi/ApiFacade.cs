namespace Services.ExternalApi;

public class ApiFacade : IApiFacade
{
    public IAuthService Auth { get; }
    public IUserService User { get; }
    public IProjectService Project { get; }

    public ApiFacade(IAuthService authService, IUserService userService,
        IProjectService projectService)
    {
        Auth = authService;
        User = userService;
        Project = projectService;
    }
}