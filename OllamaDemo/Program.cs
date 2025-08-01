using Microsoft.Extensions.Configuration;
using OllamaSharp;
using OllamaDemo;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var settings = configuration.GetSection(nameof(OllamaSettings)).Get<OllamaSettings>()
    ?? throw new InvalidOperationException("OllamaSettings section is missing in appsettings.json");

var ollama = new OllamaApiClient(new Uri(settings.BaseUrl));
ollama.SelectedModel = settings.ModelName;
        
Console.WriteLine($"Enter your message for {settings.ModelName} model (or 'exit' to quit):");

while (true)
{
    Console.Write("> ");
    string? prompt = Console.ReadLine();

    if (string.Equals(prompt, "exit", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    if (string.IsNullOrWhiteSpace(prompt))
    {
        continue;
    }

    try
    {
        Console.Write($"\n{settings.ModelName}: ");
        // Sending the request and getting the response in Streaming mode (word by word)
        await foreach (var res in ollama.GenerateAsync(prompt))
        {
            Console.Write(res.Response);
        }
        Console.WriteLine("\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
        Console.WriteLine("Make sure you ran 'ollama serve' in a separate terminal.");
    }
}