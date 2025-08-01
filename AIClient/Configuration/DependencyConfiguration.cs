using AIClient.ViewModels;
using AIServices.Configuration;
using AIServices.Interfaces;
using AIServices.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AIClient.Configuration
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddConfiguration(configuration)
                .AddLogging(configuration)
                .AddApplicationServices()
                .AddViewModels();

            return services;
        }

        private static IServiceCollection AddConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);

            services.Configure<LLMSettings>(configuration.GetSection(nameof(LLMSettings)));

            return services;
        }

        private static IServiceCollection AddLogging(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Seq(configuration.GetValue<string>("Logging:SeqServerUrl"))
                .CreateLogger();

            services.AddSingleton<ILogger>(logger);

            return services;
        }

        private static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddSingleton<ILanguageModelService, GeminiLanguageModelService>();
            return services;
        }

        private static IServiceCollection AddViewModels(
            this IServiceCollection services)
        {
            services.AddTransient<MainViewModel>();
            return services;
        }
    }
}