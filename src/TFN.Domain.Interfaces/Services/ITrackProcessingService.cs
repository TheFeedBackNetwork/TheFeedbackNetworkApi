using System.Threading.Tasks;

namespace TFN.Domain.Interfaces.Services
{
    public interface ITrackProcessingService
    {

        Task<string> TranscodeAudioAsync(string sourceFilePath, string destinationFilePath);

    }
}
