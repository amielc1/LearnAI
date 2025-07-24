using Microsoft.VisualStudio.TestTools.UnitTesting;
using AIClient.ViewModels;
using AIClient.Models;
using System.Threading.Tasks;

namespace AIClient.Tests.ViewModels
{
    [TestClass]
    public class MainViewModelTests
    {
        private MainViewModel _viewModel = null!;

        [TestInitialize]
        public void Setup()
        {
            _viewModel = new MainViewModel();
        }

        [TestMethod]
        public void Constructor_InitializesEmptyMessages()
        {
            // Assert
            Assert.IsNotNull(_viewModel.Messages);
            Assert.AreEqual(0, _viewModel.Messages.Count);
        }

        [TestMethod]
        public void Constructor_InitializesEmptyUserInput()
        {
            // Assert
            Assert.AreEqual(string.Empty, _viewModel.UserInput);
        }

        [TestMethod]
        public async Task SendAsync_WithValidInput_AddsUserMessage()
        {
            // Arrange
            string testInput = "Test message";
            _viewModel.UserInput = testInput;

            // Act
            await _viewModel.SendCommand.ExecuteAsync(null);

            // Assert
            Assert.AreEqual(2, _viewModel.Messages.Count);
            Assert.AreEqual("You", _viewModel.Messages[0].Sender);
            Assert.AreEqual(testInput, _viewModel.Messages[0].Content);
        }

        [TestMethod]
        public async Task SendAsync_WithValidInput_AddsAIResponse()
        {
            // Arrange
            string testInput = "Test message";
            _viewModel.UserInput = testInput;

            // Act
            await _viewModel.SendCommand.ExecuteAsync(null);

            // Assert
            Assert.AreEqual(2, _viewModel.Messages.Count);
            Assert.AreEqual("AI", _viewModel.Messages[1].Sender);
            Assert.AreEqual($"Echo: {testInput}", _viewModel.Messages[1].Content);
        }

        [TestMethod]
        public async Task SendAsync_WithValidInput_ClearsUserInput()
        {
            // Arrange
            _viewModel.UserInput = "Test message";

            // Act
            await _viewModel.SendCommand.ExecuteAsync(null);

            // Assert
            Assert.AreEqual(string.Empty, _viewModel.UserInput);
        }

        [TestMethod]
        public async Task SendAsync_WithEmptyInput_DoesNotAddMessages()
        {
            // Arrange
            _viewModel.UserInput = string.Empty;

            // Act
            await _viewModel.SendCommand.ExecuteAsync(null);

            // Assert
            Assert.AreEqual(0, _viewModel.Messages.Count);
        }

        [TestMethod]
        public async Task SendAsync_WithWhitespaceInput_DoesNotAddMessages()
        {
            // Arrange
            _viewModel.UserInput = "   ";

            // Act
            await _viewModel.SendCommand.ExecuteAsync(null);

            // Assert
            Assert.AreEqual(0, _viewModel.Messages.Count);
        }
    }
}