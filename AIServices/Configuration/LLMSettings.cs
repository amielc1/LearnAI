using AIServices.Configuration;

namespace AIServices.Configuration
{
    public class LLMSettings
    { 
        public string ApiKey { get; set; } = string.Empty;
        public string ModelName { get; set; } = "gemini-pro";
        public float Temperature { get; set; } = 0.7f;
        public int MaxTokens { get; set; } = 2048;
    }
} 
