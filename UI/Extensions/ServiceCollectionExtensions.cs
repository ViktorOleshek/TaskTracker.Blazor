using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using Services.ExternalApi;

namespace UI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(TaskTrackerSettings.ConfigurationSection)
            .Get<TaskTrackerSettings>();

        var refitSettings = new RefitSettings
        {
            ContentSerializer = new NewtonsoftJsonContentSerializer(
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                })
        };

        services.AddHttpClient("TaskTrackerApi", client =>
        {
            client.BaseAddress = new Uri(settings!.BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json-patch+json"));
        });

        services.AddScoped<IAuthService>(sp =>
        {
            var httpClient = sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("TaskTrackerApi");
            return RestService.For<IAuthService>(httpClient, refitSettings);
        });

        services.AddScoped<IUserService>(sp =>
        {
            var httpClient = sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("TaskTrackerApi");
            return RestService.For<IUserService>(httpClient, refitSettings);
        });

        services.AddScoped<IProjectService>(sp =>
        {
            var httpClient = sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("TaskTrackerApi");
            return RestService.For<IProjectService>(httpClient, refitSettings);
        });

        services.AddScoped<IApiFacade, ApiFacade>();

        return services;
    }
}
