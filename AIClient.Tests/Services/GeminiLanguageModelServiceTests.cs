using Microsoft.VisualStudio.TestTools.UnitTesting;
using AIServices.Interfaces;
using AIServices.Services;
using Moq;
using Serilog;

namespace AIClient.Tests.Services
{
    [TestClass]
    public class GeminiLanguageModelServiceTests
    {
        private Mock<ILanguageModelConfig> _configMock = null!;
        private Mock<ILogger> _loggerMock = null!;
        private GeminiLanguageModelService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _configMock = new Mock<ILanguageModelConfig>();
            _configMock.Setup(c => c.ApiKey).Returns("test-api-key");
            _configMock.Setup(c => c.ModelName).Returns("gemini-pro");
            _configMock.Setup(c => c.Temperature).Returns(0.7f);
            _configMock.Setup(c => c.MaxTokens).Returns(2048);

            _loggerMock = new Mock<ILogger>();
            _loggerMock.Setup(x => x.ForContext<GeminiLanguageModelService>())
                      .Returns(_loggerMock.Object);

            _service = new GeminiLanguageModelService(_configMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task GetResponseAsync_WithValidPrompt_LogsInformation()
        {
            // Arrange
            string prompt = "Test prompt";

            // Act
            try
            {
                await _service.GetResponseAsync(prompt);
            }
            catch
            {
                // We expect an exception in tests due to invalid API key
            }

            // Assert
            _loggerMock.Verify(
                x => x.Information(
                    It.IsAny<string>(),
                    It.IsAny<object[]>()
                ),
                Times.AtLeast(1)
            );
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GetResponseAsync_WithInvalidApiKey_ThrowsException()
        {
            // Arrange
            string prompt = "Test prompt";

            // Act
            await _service.GetResponseAsync(prompt);
        }
    }
}