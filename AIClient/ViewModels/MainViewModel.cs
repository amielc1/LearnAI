using AIClient.Models;
using AIServices.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;
using System.Collections.ObjectModel;

namespace AIClient.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ILanguageModelService _languageModel;
        private readonly ILogger _logger;

        [ObservableProperty]
        private string userInput = string.Empty;

        public ObservableCollection<ChatMessage> Messages { get; } = new();

        public MainViewModel(ILanguageModelService languageModel, ILogger logger)
        {
            _languageModel = languageModel;
            _logger = logger.ForContext<MainViewModel>();
        }

        [RelayCommand]
        private async Task SendAsync()
        {
            if (string.IsNullOrWhiteSpace(UserInput)) return;

            try
            {
                _logger.Information("Processing user input: {UserInput}", UserInput);

                var userMsg = new ChatMessage { Sender = "You", Content = UserInput, Timestamp = DateTime.Now };
                Messages.Add(userMsg);

                var response = await GetAIResponseAsync(UserInput);
                var aiMsg = new ChatMessage { Sender = "AI", Content = response, Timestamp = DateTime.Now };
                Messages.Add(aiMsg);

                UserInput = string.Empty;

                _logger.Information("Successfully processed user input and received AI response");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error processing user input");
                var errorMsg = new ChatMessage 
                { 
                    Sender = "System", 
                    Content = "An error occurred while processing your request.", 
                    Timestamp = DateTime.Now 
                };
                Messages.Add(errorMsg);
            }
        }

        private async Task<string> GetAIResponseAsync(string input)
        {
            return await _languageModel.GetResponseAsync(input);
        }
    }
}