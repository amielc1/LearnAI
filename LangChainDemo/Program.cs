// Program.cs

using LangChain.Chains;
using LangChain.Chains.LLM;
using LangChain.Prompts;
using LangChain.Providers.Ollama;
using LangChain.Schema;

await RunSocialMediaPostGeneratorAsync();

async Task RunSocialMediaPostGeneratorAsync()
{
    Console.WriteLine("--- LangChain.NET Demo with OLLAMA: Social Media Post Generator ---");

    try
    {
         
        //var provider = new OllamaProvider("http://localhost:11434");
   
        var provider = new OllamaProvider();  
        var model = new OllamaChatModel(provider, "llama3.2:3b");
         
        // Create a proper prompt template with input variables
        var template = "Write a short and original social media post idea about the topic: {topic}. The post should be light, engaging, and include a relevant emoji.";
        var inputVariables = new[] { "topic" };
        
        var promptTemplate = new PromptTemplate(new PromptTemplateInput(
            template,
            inputVariables
        ));

        // Create the chain with the prompt template
        var chain = new LlmChain(new LlmChainInput(model, promptTemplate));

        Console.WriteLine("\nEnter a topic for the post (e.g., 'Artificial Intelligence', 'Traveling in Italy', 'Morning Coffee'):");
        var userTopic = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(userTopic))
        {
            Console.WriteLine("No topic entered. Exiting program.");
            return;
        }

        Console.WriteLine("\n...Sending request to the local model (Ollama)...");

        // Run the chain with the input variable provided by the user
        var result = await chain.CallAsync(new ChainValues
        {
            ["topic"] = userTopic
        });

        // Print the result to the console
        Console.WriteLine("\n✨ AI-Generated Post Idea: ✨");
        Console.WriteLine("-----------------------------------");
        Console.WriteLine(result.Value["text"].ToString()?.Trim());
        Console.WriteLine("-----------------------------------");

        Console.ReadLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n❌ Error: {ex.Message}");
        Console.WriteLine("\nMake sure that:");
        Console.WriteLine("1. Ollama is running (the Ollama service should be started)");
         Console.WriteLine("3. The Ollama service is accessible at http://localhost:11434");
    }
}