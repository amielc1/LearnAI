using LangChain.Chains.LLM;
using LangChain.Schema;

namespace LangChainDemo;

public class AppRunner
{
    private readonly ILlmChain _chain;

    // The ILlmChain is now injected via the constructor
    public AppRunner(ILlmChain chain)
    {
        _chain = chain;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("--- LangChain.NET Demo: Social Media Post Generator ---");
        try
        {
            Console.WriteLine("\nEnter a topic for the post (e.g., 'Artificial Intelligence', 'Traveling in Italy'):");
            var userTopic = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userTopic))
            {
                Console.WriteLine("No topic entered. Exiting program.");
                return;
            }

            Console.WriteLine("\n...Sending request to the model...");

            var result = await _chain.CallAsync(new ChainValues
            {
                ["topic"] = userTopic
            });

            Console.WriteLine("\n✨ AI-Generated Post Idea: ✨");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(result.Value["text"].ToString()?.Trim());
            Console.WriteLine("-----------------------------------");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n❌ Error: {ex.Message}");
        }
    }
}