using AI.Core;
using AIClient.Configuration;
using AIClient.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;

namespace AIClient
{
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;
        private IConfiguration _configuration;

        public App()
        {
            _configuration = ConfigureConfiguration();
            _serviceProvider = ConfigureServices();
        }

        private IConfiguration ConfigureConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        private ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            
            // Register all application services using the extension method
            services.AddApplicationServices(_configuration);
            
            var serviceProvider = services.BuildServiceProvider();
            ServiceLocator.Initialize(serviceProvider);
            return serviceProvider;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow
            {
                DataContext = _serviceProvider.GetRequiredService<MainViewModel>()
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

