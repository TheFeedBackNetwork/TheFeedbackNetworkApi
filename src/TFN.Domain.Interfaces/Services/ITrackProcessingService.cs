using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TFN.Domain.Interfaces.Services
{
    public interface ITrackProcessingService
    {
        Task TranscodeAudioAsync(string sourceFilePath, string destinationFilePath);
        Task<List<int>> GetWaveformAsync(string sourceFilePath, string destinationFilePath);
    }
}
