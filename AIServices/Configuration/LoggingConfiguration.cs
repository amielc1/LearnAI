using Serilog;

namespace AIServices.Configuration
{
    public static class LoggingConfiguration
    {
        public static ILogger ConfigureLogging(string seqServerUrl = "http://localhost:5341")
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()  
                .WriteTo.Seq(seqServerUrl) 
                .CreateLogger();
        }
    }
}