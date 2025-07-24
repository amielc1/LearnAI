using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AIClient.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AIClient.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string userInput = string.Empty;

        public ObservableCollection<ChatMessage> Messages { get; } = new();

        [RelayCommand]
        private async Task SendAsync()
        {
            if (string.IsNullOrWhiteSpace(UserInput)) return;
            var userMsg = new ChatMessage { Sender = "You", Content = UserInput, Timestamp = DateTime.Now };
            Messages.Add(userMsg);
            var aiMsg = new ChatMessage { Sender = "AI", Content = await GetAIResponseAsync(UserInput), Timestamp = DateTime.Now };
            Messages.Add(aiMsg);
            UserInput = string.Empty;
        }

        private Task<string> GetAIResponseAsync(string input)
        {
            // TODO: Replace with real LangChain/AI call
            return Task.FromResult($"Echo: {input}");
        }
    }
}