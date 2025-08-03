using LangChain.Chains;
using LangChain.Chains.LLM;
using LangChainDemo;
using LangChainDemo.Services;
using LangChainDemo.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Use the .NET Generic Host for a modern application structure
var builder = Host.CreateDefaultBuilder(args);

// Configure the services for Dependency Injection
builder.ConfigureServices((context, services) =>
{
    // Bind the AiSettings section from appsettings.json to our AiSettings class
    services.Configure<AiSettings>(context.Configuration.GetSection("AiSettings"));

    // Register our factory as a singleton
    services.AddSingleton<LlmChainFactory>();

    // Register the ILlmChain interface.
    // The DI container will use our factory to create a single instance.
    services.AddSingleton<ILlmChain>(serviceProvider =>
    {
        var factory = serviceProvider.GetRequiredService<LlmChainFactory>();
        return factory.Create();
    });

    // Register our main application class
    services.AddTransient<AppRunner>();
});

// Build and run the host
var host = builder.Build();

// Get the AppRunner service from the DI container and run it
var app = host.Services.GetRequiredService<AppRunner>();
await app.RunAsync();