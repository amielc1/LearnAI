using OllamaSharp;

var ollama = new OllamaApiClient(new Uri("http://localhost:11434"));

ollama.SelectedModel = "llama3.2:3b";
        
Console.WriteLine("Enter your message for Llama 3.2 model (or 'exit' to quit):");

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
        Console.Write("\nLlama3.2: ");
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