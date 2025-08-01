using AI.Core;
using AIServices.Configuration;
using AIServices.Interfaces;
using Microsoft.Extensions.Options;
using Serilog;
using System.Diagnostics;

namespace AIServices.Services
{


    public class GeminiLanguageModelService : ILanguageModelService
    {
        private readonly LLMSettings _settings;
        private readonly ILogger _logger;

        public GeminiLanguageModelService(
            IOptions<LLMSettings> options,
            ILogger logger)
        {
            _settings = options.Value;
            _logger = logger.ForContext<GeminiLanguageModelService>();
        }

        public async Task<string> GetResponseAsync(string prompt)
        {
            try
            {
                _logger.Information("Sending prompt to Gemini. Length: {PromptLength}", prompt.Length);
                var stopwatch = Stopwatch.StartNew();

                // TODO: Implement actual Gemini API call using _settings
                await Task.Delay(100); // Simulate API call
                var result = $"Simulated response for prompt: {prompt}";

                stopwatch.Stop();
                _logger.Information(
                    "Received response from Gemini. Length: {ResponseLength}, Time: {ElapsedMs}ms",
                    result.Length,
                    stopwatch.ElapsedMilliseconds
                );

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting response from Gemini. Prompt: {Prompt}", prompt);
                throw;
            }
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