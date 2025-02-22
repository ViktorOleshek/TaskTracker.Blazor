using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using Services.ExternalApi;
using System.Net.Http.Headers;

namespace UI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<TokenHttpMessageHandler>();

        var settings = configuration
                        .GetSection(TaskTrackerSettings.ConfigurationSection)
                        .Get<TaskTrackerSettings>();
        services.AddHttpClient("TaskTrackerApi", client =>
        {
            client.BaseAddress = new Uri(settings!.BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json-patch+json"));
        })
                .AddHttpMessageHandler<TokenHttpMessageHandler>();

        var refitSettings = new RefitSettings
        {
            ContentSerializer = new NewtonsoftJsonContentSerializer(
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver(),
                        NullValueHandling = NullValueHandling.Ignore
                    })
        };
        services.AddRefitService<IAuthService>(refitSettings);
        services.AddRefitService<IUserService>(refitSettings);
        services.AddRefitService<IProjectService>(refitSettings);

        services.AddScoped<IApiFacade, ApiFacade>();

        return services;
    }
    private static IServiceCollection AddRefitService<T>(this IServiceCollection services, RefitSettings settings)
        where T : class
    {
        services.AddScoped<T>(sp =>
        {
            var httpClient = sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("TaskTrackerApi");
            return RestService.For<T>(httpClient, settings);
        });

        return services;
    }
}