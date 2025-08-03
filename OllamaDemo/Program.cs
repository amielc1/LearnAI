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

var conversationHistory = new List<ChatMessage>();
// Add a system message to set the context
conversationHistory.Add(new ChatMessage("system", "You are a helpful AI assistant. Keep your responses concise and clear."));

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
        // Add user message to history
        conversationHistory.Add(new ChatMessage("user", prompt));

        // Construct the full context from conversation history
        var fullPrompt = string.Join("\n", conversationHistory.Select(m => $"{m.Role}: {m.Content}"));
        
        Console.Write($"\n{settings.ModelName}: ");
        string completeResponse = "";

        // Sending the request and getting the response in Streaming mode
        await foreach (var res in ollama.GenerateAsync(fullPrompt))
        {
            Console.Write(res.Response);
            completeResponse += res.Response;
        }
        Console.WriteLine("\n");

        // Add assistant's response to history
        conversationHistory.Add(new ChatMessage("assistant", completeResponse));

        // Keep conversation history manageable (last 10 messages)
        //if (conversationHistory.Count > 11) // 11 because we want to keep the system message and last 10 exchanges
        //{
        //    conversationHistory.RemoveRange(1, conversationHistory.Count - 11);
        //}
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
        Console.WriteLine("Make sure you ran 'ollama serve' in a separate terminal.");
    }
}