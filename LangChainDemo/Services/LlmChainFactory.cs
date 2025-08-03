using LangChain.Chains.LLM;
using LangChain.Prompts;
using LangChain.Providers;
using LangChain.Providers.Google;
using LangChain.Providers.Ollama;
using LangChainDemo.Settings;
using Microsoft.Extensions.Options;

namespace LangChainDemo.Services;

public class LlmChainFactory
{
    private readonly AiSettings _settings;

    public LlmChainFactory(IOptions<AiSettings> settings)
    {
        // Use IOptions for safe access to configured settings
        _settings = settings.Value;
    }

    public ILlmChain Create()
    {
        // 1. Create the correct chat model based on the configuration
        var model = CreateChatModel();
        
        // 2. Define the prompt template (this is common logic)
        var template = "Write a short and original social media post idea about the topic: {topic}. The post should be light, engaging, and include a relevant emoji.";
        var promptTemplate = new PromptTemplate(new PromptTemplateInput(
            template,
            inputVariables: new[] { "topic" }
        ));

        // 3. Create and return the chain
        return new LlmChain(new LlmChainInput(model, promptTemplate));
    }

    private IChatModel CreateChatModel()
    {
        return _settings.ProviderType switch
        {
            ProviderType.Ollama => new OllamaChatModel(
                new OllamaProvider(), _settings.Ollama.ModelId!),

            //ProviderType.Google => new GoogleChatModel(
            //    new GoogleProvider(), _settings.Google.ModelId!)
            //{
            //    ApiKey = _settings.Google.ApiKey!
            //},

            _ => throw new ArgumentOutOfRangeException(
                nameof(_settings.ProviderType), "AI Provider not supported.")
        };
    }
} 
