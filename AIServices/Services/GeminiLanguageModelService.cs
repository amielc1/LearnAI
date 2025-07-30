using AIServices.Interfaces;
//using Google.Generative.AI;
using Serilog;
using System.Diagnostics;

namespace AIServices.Services
{


    public class GeminiLanguageModelService : ILanguageModelService
    {

        public Task<string> GetResponseAsync(string prompt)
        {
            //_logger.Information("Sending prompt to Gemini. Length: {PromptLength}", prompt.Length);
            return Task.FromResult($"Simulated response for prompt: {prompt}");
        }
    }

    //public class GeminiLanguageModelService : ILanguageModelService
    //{
    //    private readonly ILanguageModelConfig _config;
    //    private readonly ILogger _logger;
    //    private readonly GenerativeModel _model;

    //    public GeminiLanguageModelService(ILanguageModelConfig config, ILogger logger)
    //    {
    //        _config = config;
    //        _logger = logger.ForContext<GeminiLanguageModelService>();

    //        var settings = new GenerativeModelSettings
    //        {
    //            Temperature = _config.Temperature,
    //            MaxOutputTokens = _config.MaxTokens
    //        };

    //        _model = new GenerativeModel(_config.ModelName, _config.ApiKey, settings);
    //    }

    //    public async Task<string> GetResponseAsync(string prompt)
    //    {
    //        try
    //        {
    //            _logger.Information("Sending prompt to Gemini. Length: {PromptLength}", prompt.Length);
    //            var stopwatch = Stopwatch.StartNew();

    //            var response = await _model.GenerateContentAsync(prompt);
    //            var result = response.Text;

    //            stopwatch.Stop();
    //            _logger.Information(
    //                "Received response from Gemini. Length: {ResponseLength}, Time: {ElapsedMs}ms",
    //                result.Length,
    //                stopwatch.ElapsedMilliseconds
    //            );

    //            return result;
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.Error(ex, "Error getting response from Gemini. Prompt: {Prompt}", prompt);
    //            throw;
    //        }
    //    }
    //}
}