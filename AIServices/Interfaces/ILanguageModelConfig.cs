namespace AIServices.Interfaces
{
    public interface ILanguageModelConfig
    {
        string ApiKey { get; }
        string ModelName { get; }
        float Temperature { get; }
        int MaxTokens { get; }
    }
}