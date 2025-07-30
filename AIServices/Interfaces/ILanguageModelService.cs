using System.Threading.Tasks;

namespace AIServices.Interfaces
{
    public interface ILanguageModelService
    {
        Task<string> GetResponseAsync(string prompt);
    }
}