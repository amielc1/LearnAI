using AI.Core;
using AIClient.ViewModels;
using AIServices.Configuration;
using AIServices.Interfaces;
using AIServices.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Windows;

namespace AIClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            Services = ConfigureServices();
        }

        public ServiceProvider Services { get; }

        private ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Register Configuration
            var geminiConfig = new GeminiConfig(
                apiKey: "your-api-key-here", // TODO: Move to configuration
                modelName: "gemini-pro",
                temperature: 0.7f,
                maxTokens: 2048
            );

            services.AddSingleton<ILanguageModelConfig>(geminiConfig);

            // Register Logger
            var logger = ConfigureLogging();   
            services.AddSingleton<ILogger>(logger);

            // Register Services
            services.AddSingleton<ILanguageModelService, GeminiLanguageModelService>();
            
            // Register ViewModels
            services.AddTransient<MainViewModel>();
            var serviceProvider = services.BuildServiceProvider(); 
            ServiceLocator.Initialize(serviceProvider);
            return serviceProvider;
        }

         static ILogger ConfigureLogging(string seqServerUrl = "http://localhost:5341")
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Seq(seqServerUrl)
                .CreateLogger();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow
            {
                DataContext = Services.GetRequiredService<MainViewModel>()
            };

            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _serviceProvider?.Dispose();
        }
    }
}
