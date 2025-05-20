using FakeStoreApi.Integrations.Configs;
using FakeStoreApi.Integrations.Repositories;
using FakeStoreApi.Integrations.Repositories.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace FakeStoreApi.Integrations;

public static class TestContainer
{
    public static IHost TestHost { get; set; }

    public static void Initialize(string environment = null)
    {
        TestHost = Host.CreateDefaultBuilder().ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.json");
        })
        .ConfigureServices((context, services) =>
        {
            var configuration = context.Configuration.GetSection(ApiConfig.API);
            services.Configure<ApiConfig>(configuration);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<ApiConfig>>().Value);

            services.AddSingleton(provider => new HttpClient());
            services.AddSingleton<IProductApiHelper, ProductApiHelper>();
            services.AddSingleton<ICartApiHelper, CartApiHelper>();
            services.AddSingleton<IAuthApiHelper, AuthApiHelper>();
            services.AddSingleton<IUserApiHelper, UserApiHelper>();
        })
        .Build();
    }
}
