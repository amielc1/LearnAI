using AIServices.Interfaces;

namespace AIServices.Configuration
{
    public class GeminiConfig : ILanguageModelConfig
    {
        public string ApiKey { get; }
        public string ModelName { get; }
        public float Temperature { get; }
        public int MaxTokens { get; }

        public GeminiConfig(string apiKey, string modelName = "gemini-pro", float temperature = 0.7f, int maxTokens = 2048)
        {
            ApiKey = apiKey;
            ModelName = modelName;
            Temperature = temperature;
            MaxTokens = maxTokens;
        }
    }
}