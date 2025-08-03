namespace LangChainDemo.Settings;

public class AiSettings
{
    public ProviderType ProviderType { get; set; }
    public OllamaSettings Ollama { get; set; } = new();
    public GoogleSettings Google { get; set; } = new();
}

public class OllamaSettings
{
    public string? ModelId { get; set; }
}

public class GoogleSettings
{
    public string? ModelId { get; set; }
    public string? ApiKey { get; set; }
}

public enum ProviderType
{
    Ollama,
    Google
}