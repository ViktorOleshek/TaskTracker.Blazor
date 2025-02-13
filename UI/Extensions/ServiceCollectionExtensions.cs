using Refit;
using Services.Abstraction;
using Services.ExternalApi;

namespace UI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(TaskTrackerSettings.ConfigurationSection)
            .Get<TaskTrackerSettings>();

        services.AddHttpClient("TaskTrackerApi", client =>
        {
            client.BaseAddress = new Uri(settings!.BaseAddress);
        });

        services.AddScoped<IAuthService>(sp =>
        {
            var httpClient = sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("TaskTrackerApi");
            return RestService.For<IAuthService>(httpClient);
        });

        services.AddScoped<IUserService>(sp =>
        {
            var httpClient = sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("TaskTrackerApi");
            return RestService.For<IUserService>(httpClient);
        });

        services.AddScoped<IApiFacade, ApiFacade>();

        return services;
    }
}